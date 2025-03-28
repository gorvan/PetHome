# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/PetHome.Web/PetHome.Web.csproj", "src/PetHome.Web/"]
COPY ["src/PetHome.Disscusions.Infrastructure/PetHome.Disscusions.Infrastructure.csproj", "src/PetHome.Disscusions.Infrastructure/"]
COPY ["src/PetHome.Disscusions.Application/PetHome.Disscusions.Application.csproj", "src/PetHome.Disscusions.Application/"]
COPY ["src/PetHome.Disscusions.Domain/PetHome.Disscusions.Domain.csproj", "src/PetHome.Disscusions.Domain/"]
COPY ["src/PetHome.Accounts.Contracts/PetHome.Accounts.Contracts.csproj", "src/PetHome.Accounts.Contracts/"]
COPY ["src/PetHome.Accounts.Domain/PetHome.Accounts.Domain.csproj", "src/PetHome.Accounts.Domain/"]
COPY ["src/PetHome.Shared.Core/PetHome.Shared.Core.csproj", "src/PetHome.Shared.Core/"]
COPY ["src/PetHome.Shared.Framework/PetHome.Shared.Framework.csproj", "src/PetHome.Shared.Framework/"]
COPY ["src/PetHome.Species.Application/PetHome.Species.Application.csproj", "src/PetHome.Species.Application/"]
COPY ["src/PetHome.Volunteers.Contracts/PetHome.Volunteers.Contracts.csproj", "src/PetHome.Volunteers.Contracts/"]
COPY ["src/PetHome.Species.Domain/PetHome.Species.Domain.csproj", "src/PetHome.Species.Domain/"]
COPY ["src/PetHome.Disscusions.Presentation/PetHome.Disscusions.Presentation.csproj", "src/PetHome.Disscusions.Presentation/"]
COPY ["src/PetHome.Disscusions.Contracts/PetHome.Disscusions.Contracts.csproj", "src/PetHome.Disscusions.Contracts/"]
COPY ["src/PetHome.Shared.SharedKernel/PetHome.Shared.SharedKernel.csproj", "src/PetHome.Shared.SharedKernel/"]
COPY ["src/PetHome.Accounts.Infrastructure/PetHome.Accounts.Infrastructure.csproj", "src/PetHome.Accounts.Infrastructure/"]
COPY ["src/PetHome.Accounts.Presentation/PetHome.Accounts.Presentation.csproj", "src/PetHome.Accounts.Presentation/"]
COPY ["src/PetHome.Accounts.Application/PetHome.Accounts.Application.csproj", "src/PetHome.Accounts.Application/"]
COPY ["src/PetHome.Species.Infrastructure/PetHome.Species.Infrastructure.csproj", "src/PetHome.Species.Infrastructure/"]
COPY ["src/PetHome.VolunteerRequest.Infrastructure/PetHome.VolunteerRequests.Infrastructure.csproj", "src/PetHome.VolunteerRequest.Infrastructure/"]
COPY ["src/PetHome.VolunteerRequest.Application/PetHome.VolunteerRequests.Application.csproj", "src/PetHome.VolunteerRequest.Application/"]
COPY ["src/PetHome.VolunteerRequest.Domain/PetHome.VolunteerRequests.Domain.csproj", "src/PetHome.VolunteerRequest.Domain/"]
COPY ["src/PetHome.VolunteerRequest.Presentation/PetHome.VolunteerRequests.Presentation.csproj", "src/PetHome.VolunteerRequest.Presentation/"]
COPY ["src/PetHome.Volunteers.Application/PetHome.Volunteers.Application.csproj", "src/PetHome.Volunteers.Application/"]
COPY ["src/PetHome.Volunteers.Domain/PetHome.Volunteers.Domain.csproj", "src/PetHome.Volunteers.Domain/"]
COPY ["src/PetHome.Species.Contracts/PetHome.Species.Contracts.csproj", "src/PetHome.Species.Contracts/"]
COPY ["src/PetHome.Species.Presentation/PetHome.Species.Presentation.csproj", "src/PetHome.Species.Presentation/"]
COPY ["src/PetHome.Volunteers.Infrastructure/PetHome.Volunteers.Infrastructure.csproj", "src/PetHome.Volunteers.Infrastructure/"]
COPY ["src/PetHome.Volunteers.Presentation/PetHome.Volunteers.Presentation.csproj", "src/PetHome.Volunteers.Presentation/"]
RUN dotnet restore "./src/PetHome.Web/PetHome.Web.csproj"
COPY . .
WORKDIR "/src/src/PetHome.Web"
RUN dotnet build "./PetHome.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PetHome.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetHome.Web.dll"]