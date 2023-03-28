FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src
COPY ["src/OrderManager.Client/OrderManager.Client.csproj", "src/OrderManager.Client/"]
COPY ["src/OrderManager.Client.Integration.Http/OrderManager.Client.Integration.Http.csproj", "src/OrderManager.Client.Integration.Http/"]
RUN dotnet restore "src/OrderManager.Client/OrderManager.Client.csproj"
COPY . .
WORKDIR "/src/src/OrderManager.Client"
RUN dotnet build "OrderManager.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderManager.Client.csproj" -c Release -o /app/publish

FROM nginx:alpine
WORKDIR /var/www/web
ENV NGINX_ENVSUBST_OUTPUT_DIR=/var/www/web

COPY --from=publish /app/publish/wwwroot .
COPY src/OrderManager.Client/nginx.conf /etc/nginx/nginx.conf
COPY src/OrderManager.Client/appsettings.json.template /etc/nginx/templates/

EXPOSE 80
