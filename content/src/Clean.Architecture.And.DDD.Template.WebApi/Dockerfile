FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/Clean.Architecture.And.DDD.Template.Domian/Clean.Architecture.And.DDD.Template.Domian.csproj", "src/Clean.Architecture.And.DDD.Template.Domian/"]
COPY ["src/Clean.Architecture.And.DDD.Template.Application/Clean.Architecture.And.DDD.Template.Application.csproj", "src/Clean.Architecture.And.DDD.Template.Application/"]
COPY ["src/Clean.Architecture.And.DDD.Template.Infrastructure/Clean.Architecture.And.DDD.Template.Infrastructure.csproj", "src/Clean.Architecture.And.DDD.Template.Infrastructure/"]
COPY ["src/Clean.Architecture.And.DDD.Template.WebApi/Clean.Architecture.And.DDD.Template.WebApi.csproj", "src/Clean.Architecture.And.DDD.Template.WebApi/"]

RUN dotnet restore "./src/Clean.Architecture.And.DDD.Template.WebApi/Clean.Architecture.And.DDD.Template.WebApi.csproj" --disable-parallel
COPY . .
WORKDIR "src/Clean.Architecture.And.DDD.Template.WebApi"

RUN dotnet publish "./Clean.Architecture.And.DDD.Template.WebApi.csproj" -c release -o /app #--no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
COPY --from=build /app ./

EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ["dotnet", "Clean.Architecture.And.DDD.Template.WebApi.dll"]