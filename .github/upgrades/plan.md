# .NET 10 Migration & Modernization Plan

## Executive Summary

Comprehensive upgrade plan for BlogManagement solution from .NET 8 to .NET 10 LTS, including Identity fixes, Swagger ? Scalar migration, and Docker updates.

**Target Framework**: .NET 10.0 (LTS)  
**Scope**: All 6 projects + Docker configuration  
**Estimated Effort**: ~2-3 hours  
**Risk Level**: Low (Clean Architecture, modern SDK-style projects)

---

## Phase 1: Pre-Upgrade Preparation (Foundation)

### 1.1 Validate .NET 10 SDK Installation
- Verify .NET 10.0 SDK is installed on the machine
- Validate .NET 10 SDKs and tools are available
- Check global.json compatibility (if present)

### 1.2 Update Project Target Frameworks
Update TargetFramework in all .csproj files:
- `src\SharedKarnel\SharedKernel.csproj` - net8.0 ? net10.0
- `src\CleanArchitecture.BlogManagement.Core\CleanArchitecture.BlogManagement.Core.csproj` - net8.0 ? net10.0
- `src\CleanArchitecture.BlogManagement.Application\CleanArchitecture.BlogManagement.Application.csproj` - net8.0 ? net10.0
- `src\CleanArchitecture.BlogManagement.Infrastructure\CleanArchitecture.BlogManagement.Infrastructure.csproj` - net8.0 ? net10.0
- `tests\CleanArchitecture.BlogManagement.Core.UnitTest\CleanArchitecture.BlogManagement.Core.UnitTest.csproj` - net8.0 ? net10.0
- `src\CleanArchitecture.BlogManagement.WebApi\CleanArchitecture.BlogManagement.WebApi.csproj` - net8.0 ? net10.0

---

## Phase 2: Package Updates (Dependencies)

### 2.1 Core Framework Package Upgrades
All projects must update these Microsoft packages to **10.0.2**:

| Package | Projects | Current | Target | Priority |
|---------|----------|---------|--------|----------|
| Microsoft.EntityFrameworkCore | Core, SharedKernel | 8.0.1 | 10.0.2 | ??? Critical |
| Microsoft.EntityFrameworkCore.SqlServer | Infrastructure | 8.0.1 | 10.0.2 | ??? Critical |
| Microsoft.EntityFrameworkCore.Design | WebApi | 8.0.1 | 10.0.2 | ??? Critical |
| Microsoft.EntityFrameworkCore.Tools | Infrastructure | 8.0.1 | 10.0.2 | ??? Critical |
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | Infrastructure | 8.0.1 | 10.0.2 | ??? IDENTITY |
| Microsoft.AspNetCore.Authentication.JwtBearer | WebApi | 8.0.1 | 10.0.2 | ??? Auth |
| Microsoft.Extensions.Logging.Abstractions | Application | 8.0.0 | 10.0.2 | ?? Important |
| Newtonsoft.Json | Application | 13.0.3 | 13.0.4 | ? Optional |

### 2.2 API Documentation Package Migration
**CRITICAL: Remove Swagger, Add Scalar**
- Remove: `Swashbuckle.AspNetCore` (6.5.0) from WebApi
- Add: `Scalar.AspNetCore` (latest) to WebApi
- Keep: `Microsoft.AspNetCore.OpenApi` (upgrade to 10.0.2)

### 2.3 Docker Support Package
**CRITICAL: Remove incompatible package**
- Remove: `Microsoft.VisualStudio.Azure.Containers.Tools.Targets` (1.19.6)
  - Reason: Incompatible with .NET 10
  - Alternative: Dockerfile will use standard base images

### 2.4 Deprecated Package Notices
?? Optional (can keep for now, but note for future):
- `AutoMapper.Extensions.Microsoft.DependencyInjection` (12.0.1) - Deprecated
- `FluentValidation.AspNetCore` (11.3.0) - Deprecated

---

## Phase 3: Code Updates (Identity & API Configuration)

### 3.1 Identity Configuration (Infrastructure)
**File**: `src\CleanArchitecture.BlogManagement.Infrastructure\DependencyInjection.cs`

Current Identity setup is compatible with .NET 10:
- ? `AddIdentityCore<ApplicationUser>()`
- ? `.AddEntityFrameworkStores<BlogDbContext>()`
- ? `.AddDefaultTokenProviders()`
- ? `.AddApiEndpoints()`
- ? `AddBearerToken()` authentication

**Action**: Update package reference only, no code changes needed for Identity core functionality.

### 3.2 Swagger ? Scalar Migration (WebApi)
**File**: `src\CleanArchitecture.BlogManagement.WebApi\DependencyInjection.cs`

Current Swagger setup:
```csharp
public static IServiceCollection RegisterSwagger(this IServiceCollection services)
{
    services.AddSwaggerGen(opt => { ... });
}
```

**Remove entire Swagger registration method and replace with Scalar:**
```csharp
public static IServiceCollection RegisterScalar(this IServiceCollection services)
{
    services.AddOpenApi();
    return services;
}
```

**File**: `src\CleanArchitecture.BlogManagement.WebApi\Program.cs`

Current usage:
```csharp
builder.Services.RegisterSwagger();
app.UseSwagger();
app.UseSwaggerUI();
app.MapEndpoints();
```

**Replace with Scalar:**
```csharp
builder.Services.RegisterScalar();
app.MapOpenApi();
app.MapScalarApiReference();
app.MapEndpoints();
```

### 3.3 Program.cs Updates for Exception Handling
.NET 10 has improved exception handling with `IExceptionHandler`. Current `GlobalExceptionHandler` should remain compatible.

---

## Phase 4: Docker Updates

### 4.1 Dockerfile Update
**File**: `src\CleanArchitecture.BlogManagement.WebApi\Dockerfile`

Update base and SDK images:
```dockerfile
# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base

# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
```

### 4.2 docker-compose.yml Configuration
**File**: `docker-compose.yml`

Current setup uses environment variables and port mappings. No changes needed for .NET 10 compatibility as the Dockerfile will use the correct base images.

---

## Phase 5: Build & Verification

### 5.1 Clean and Restore
```bash
dotnet clean
dotnet restore
```

### 5.2 Build All Projects
```bash
dotnet build --configuration Release
```

Expected outcome:
- ? 0 errors
- ?? Warnings acceptable if related to deprecations

### 5.3 Run Unit Tests
```bash
dotnet test
```

### 5.4 Docker Build Verification
```bash
docker-compose build
```

---

## Phase 6: Final Validation

### 6.1 API Health Check
- Start application
- Test Bearer token authentication
- Verify Scalar API documentation loads at `/scalar/v1`
- Test a few API endpoints

### 6.2 Database Migration
- Verify Entity Framework Core 10 migrations work
- Confirm application database context initializes correctly

### 6.3 Docker Container Health
- Start containers via docker-compose
- Verify WebApi container starts without errors
- Check logs for any runtime issues

---

## Rollback Strategy

If critical issues occur:
1. All changes on `upgrade-to-NET10` branch
2. Switch back to `master` branch: `git checkout master`
3. No production impact (this is a separate branch)
4. Can investigate and retry from any commit point

---

## Success Criteria

? All projects build with 0 errors  
? All unit tests pass  
? Scalar API documentation accessible  
? Bearer token authentication functional  
? Entity Framework Core migrations run successfully  
? Docker containers start and run without errors  
? No breaking changes to public APIs  

---

## Known Challenges & Mitigations

| Challenge | Mitigation |
|-----------|-----------|
| Binary incompatible APIs (2 instances) | Identified in assessment; will fix during execution |
| Source incompatible APIs (4 instances) | Will address during build phase; mostly TimeSpan changes |
| Behavioral changes (.NET 10) | Comprehensive testing phase validates runtime behavior |

---

## Timeline

- **Phase 1**: ~10 minutes
- **Phase 2**: ~15 minutes  
- **Phase 3**: ~20 minutes
- **Phase 4**: ~5 minutes
- **Phase 5**: ~15 minutes
- **Phase 6**: ~20 minutes

**Total**: ~85 minutes

---

## References

- Assessment: `.github/upgrades/assessment.md`
- .NET 10 Release Notes: https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10
- Scalar.AspNetCore: https://github.com/scalar/scalar
- Entity Framework Core 10: https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0
