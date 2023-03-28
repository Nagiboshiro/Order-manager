FROM mcr.microsoft.com/dotnet/runtime:7.0-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src
COPY ["src/OrderManager.Migrator/OrderManager.Migrator.csproj", "src/OrderManager.Migrator/"]
COPY ["src/OrderManager.DataAccess.Postgres/OrderManager.DataAccess.Postgres.csproj", "src/OrderManager.DataAccess.Postgres/"]
COPY ["src/OrderManager.DataAccess/OrderManager.DataAccess.csproj", "src/OrderManager.DataAccess/"]
COPY ["src/OrderManager.Domain/OrderManager.Domain.csproj", "src/OrderManager.Domain/"]
RUN dotnet restore "src/OrderManager.Migrator/OrderManager.Migrator.csproj"
COPY . .
WORKDIR "/src/src/OrderManager.Migrator"
RUN dotnet build "OrderManager.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderManager.Migrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderManager.Migrator.dll"]
