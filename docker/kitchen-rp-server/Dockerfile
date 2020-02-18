FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY ./KitchenRP.DataAccess/KitchenRP.DataAccess.csproj ./KitchenRP.DataAccess/
COPY ./KitchenRP.Web/KitchenRP.Web.csproj ./KitchenRP.Web/
COPY ./KitchenRP.Domain/KitchenRP.Domain.csproj ./KitchenRP.Domain/

WORKDIR ./KitchenRP.Web/
RUN dotnet restore

WORKDIR /app

COPY ./KitchenRP.DataAccess/. ./KitchenRP.DataAccess
COPY ./KitchenRP.Web/. ./KitchenRP.Web
COPY ./KitchenRP.Domain/. ./KitchenRP.Domain

RUN dotnet publish -c Release -o out KitchenRP.Web

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "KitchenRP.Web.dll"]
