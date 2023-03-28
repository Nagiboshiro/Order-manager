FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src
COPY ["src/OrderManager/OrderManager.csproj", "src/OrderManager/"]
COPY ["src/OrderManager.Application/OrderManager.Application.csproj", "src/OrderManager.Application/"]
COPY ["src/OrderManager.Domain/OrderManager.Domain.csproj", "src/OrderManager.Domain/"]
COPY ["src/OrderManager.DataAccess/OrderManager.DataAccess.csproj", "src/OrderManager.DataAccess/"]
COPY ["src/OrderManager.DataAccess.Postgres/OrderManager.DataAccess.Postgres.csproj", "src/OrderManager.DataAccess.Postgres/"]
RUN dotnet restore "src/OrderManager/OrderManager.csproj"
COPY . .
WORKDIR "/src/src/OrderManager"
RUN dotnet build "OrderManager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderManager.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderManager.dll"]
