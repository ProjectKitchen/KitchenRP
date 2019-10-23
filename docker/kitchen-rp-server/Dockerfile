FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app

COPY ./KitchenRP.DataAccess/KitchenRP.DataAccess.csproj ./KitchenRP.DataAccess/
COPY ./KitchenRP.Web/KitchenRP.Web.csproj ./KitchenRP.Web/
COPY ./KitchenRP.Domain/KitchenRP.Domain.csproj ./KitchenRP.Domain/
COPY ./KitchenRP.Test/KitchenRP.Test.csproj ./KitchenRP.Test/

WORKDIR ./KitchenRP.Web/
RUN dotnet restore

WORKDIR /app

COPY ./KitchenRP.DataAccess/. ./KitchenRP.DataAccess
COPY ./KitchenRP.Web/. ./KitchenRP.Web
COPY ./KitchenRP.Domain/. ./KitchenRP.Domain
COPY ./KitchenRP.Test/. ./KitchenRP.Test

RUN dotnet publish -c Release -o out KitchenRP.Web

FROM build as testrunner
WORKDIR ./KitchenRP.Test/
ENTRYPOINT ["dotnet", "test", "--logger:trx"]

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "KitchenRP.Web.dll"]
