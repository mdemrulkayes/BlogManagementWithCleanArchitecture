#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["src/CleanArchitecture.BlogManagement.WebApi/CleanArchitecture.BlogManagement.WebApi.csproj", "src/CleanArchitecture.BlogManagement.WebApi/"]
COPY ["src/CleanArchitecture.BlogManagement.Application/CleanArchitecture.BlogManagement.Application.csproj", "src/CleanArchitecture.BlogManagement.Application/"]
COPY ["src/CleanArchitecture.BlogManagement.Core/CleanArchitecture.BlogManagement.Core.csproj", "src/CleanArchitecture.BlogManagement.Core/"]
COPY ["src/SharedKarnel/SharedKernel.csproj", "src/SharedKarnel/"]
COPY ["src/CleanArchitecture.BlogManagement.Infrastructure/CleanArchitecture.BlogManagement.Infrastructure.csproj", "src/CleanArchitecture.BlogManagement.Infrastructure/"]
RUN dotnet restore "./src/CleanArchitecture.BlogManagement.WebApi/./CleanArchitecture.BlogManagement.WebApi.csproj"
COPY . .
WORKDIR "/src/src/CleanArchitecture.BlogManagement.WebApi"
RUN dotnet build "./CleanArchitecture.BlogManagement.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CleanArchitecture.BlogManagement.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArchitecture.BlogManagement.WebApi.dll"]