FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/CA.And.DDD.Template.Domain/CA.And.DDD.Template.Domain.csproj", "src/CA.And.DDD.Template.Domain/"]
COPY ["src/CA.And.DDD.Template.Application/CA.And.DDD.Template.Application.csproj", "src/CA.And.DDD.Template.Application/"]
COPY ["src/CA.And.DDD.Template.Infrastructure/CA.And.DDD.Template.Infrastructure.csproj", "src/CA.And.DDD.Template.Infrastructure/"]
COPY ["src/CA.And.DDD.Template.WebApi/CA.And.DDD.Template.WebApi.csproj", "src/CA.And.DDD.Template.WebApi/"]

RUN dotnet restore "./src/CA.And.DDD.Template.WebApi/CA.And.DDD.Template.WebApi.csproj" --disable-parallel
COPY . .
WORKDIR "src/CA.And.DDD.Template.WebApi"

RUN dotnet publish "./CA.And.DDD.Template.WebApi.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
COPY --from=build /app ./

EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ["dotnet", "CA.And.DDD.Template.WebApi.dll"]