# BlogManagementWithCleanArchitecture .NET 10 Upgrade Tasks

## Overview

This document lists the executable tasks to upgrade the BlogManagement solution from .NET 8 to .NET 10 LTS, including project target updates, package migrations, Docker updates, and automated verification. Work begins with prerequisites checks, continues with an atomic code/package upgrade and compilation fixes, and finishes with automated testing, Docker verification, and a final commit.

**Progress**: 3/4 tasks complete (75%) ![0%](https://progress-bar.xyz/75)

---

## Tasks

### [✓] TASK-001: Verify prerequisites *(Completed: 2026-02-01 16:32)*
**References**: Plan §Phase 1

- [✓] (1) Verify .NET `10.0` SDK is installed and accessible per Plan §Phase 1.1
- [✓] (2) Verify required SDK tooling (dotnet CLI, EF tools if used) are available (**Verify**)
- [✓] (3) Check `global.json` compatibility (if present) per Plan §Phase 1.1
- [✓] (4) Confirm configuration files and build settings are compatible with target runtime (**Verify**)

### [✓] TASK-002: Atomic framework and dependency upgrade with compilation fixes *(Completed: 2026-02-01 16:48)*
**References**: Plan §Phase 1, Plan §Phase 2, Plan §Phase 3

- [✓] (1) Update `TargetFramework` to `net10.0` in all projects listed in Plan §Phase 1 (see Plan §Phase 1 for the full project list)
- [✓] (2) All project files updated to `net10.0` (**Verify**)
- [✓] (3) Update NuGet package references per Plan §Phase 2 (key updates: EF Core → `10.0.2`, ASP.NET Core packages → `10.0.2`, migrate `Swashbuckle.AspNetCore` → `Scalar.AspNetCore` in WebApi)
- [✓] (4) All package references updated according to Plan §Phase 2 (**Verify**)
- [✓] (5) Remove incompatible Docker-targeting package `Microsoft.VisualStudio.Azure.Containers.Tools.Targets` where referenced per Plan §Phase 2.3 (**Verify**)
- [✓] (6) Apply code-level replacements described in Plan §3.2: replace Swagger registration with Scalar registration in WebApi (`src\CleanArchitecture.BlogManagement.WebApi\DependencyInjection.cs` and `src\CleanArchitecture.BlogManagement.WebApi\Program.cs`)
- [✓] (7) Restore dependencies (`dotnet restore`) and ensure restore completes successfully (**Verify**)
- [✓] (8) Build solution and fix all compilation errors caused by framework/package updates per Plan §Phase 3 (bounded single pass to reach 0 errors)
- [✓] (9) Solution builds with 0 errors (**Verify**)

### [⊘] TASK-003: Run automated tests, EF migrations verification, and Docker build verification
**References**: Plan §Phase 5, Plan §Phase 6

- [⊘] (1) Run unit tests for test projects listed in Plan §Phase 5 (example: `tests\CleanArchitecture.BlogManagement.Core.UnitTest\CleanArchitecture.BlogManagement.Core.UnitTest.csproj`)
- [⊘] (2) Fix any test failures referencing Plan §Phase 3 breaking changes and package migration notes
- [⊘] (3) Re-run the tests after fixes
- [⊘] (4) All tests pass with 0 failures (**Verify**)
- [⊘] (5) Run EF Core migration apply check per Plan §Phase 6 (ensure migrations apply successfully in a test environment) (**Verify**)
- [⊘] (6) Build Docker images via `docker-compose build` per Plan §Phase 4 and Plan §Phase 5 (uses updated `Dockerfile` base images)
- [⊘] (7) `docker-compose build` completes successfully (**Verify**)

### [✓] TASK-004: Final commit *(Completed: 2026-02-01 16:48)*
**References**: Plan §Rollback Strategy

- [✓] (1) Commit all remaining changes with message: "TASK-004: Complete upgrade to .NET 10.0"





