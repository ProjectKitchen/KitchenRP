FROM mcr.microsoft.com/dotnet/core/skd:3.0 AS build
WORKDIR /app

COPY ../../KitchenRP.DataAccess/KitchenRP.DataAccess.csproj ./KitchenRP.DataAccess/
COPY ../../KitchenRP.Web/KitchenRP.Web.csproj ./KitchenRP.Web/
COPY ../../KitchenRP.Domain/KitchenRP.Domain.csproj ./KitchenRP.Domain/
COPY ../../KitchenRP.Test/KitchenRP.Test.csproj ./KitchenRP.Test/

WORKDIR ./KitchenRP.Web/
RUN dotnet restore

WORKDIR /app

COPY ../../KitchenRP.DataAccess/. ./KitchenRP.DataAccess
COPY ../../KitchenRP.Web/. ./KitchenRP.Web
COPY ../../KitchenRP.Domain/. ./KitchenRP.Domain
COPY ../../KitchenRP.Test/. ./KitchenRP.Test

RUN dotnet publish -c Release -o out

FROM build as testrunner
WORKDIR ./KitchenRP.Test/
ENTRYPOINT ["dotnet", "test", "--logger:trx"]

FROM mcr.microsoft.com/dotnet/core/runtime:3.0 AS runtime
WORKDIR /app
COPY --from=build /app/dotnetapp/out ./
ENTRYPOINT ["dotnet", "kitchenRP.Web.dll"]