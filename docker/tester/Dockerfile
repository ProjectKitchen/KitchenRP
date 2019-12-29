FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /app

COPY ./KitchenRP.DataAccess/KitchenRP.DataAccess.csproj ./KitchenRP.DataAccess/
COPY ./KitchenRP.Web/KitchenRP.Web.csproj ./KitchenRP.Web/
COPY ./KitchenRP.Domain/KitchenRP.Domain.csproj ./KitchenRP.Domain/
COPY ./KitchenRP.Test/KitchenRP.Test.csproj ./KitchenRP.Test/
COPY ./KitchenRP.sln ./

RUN dotnet restore

COPY ./KitchenRP.DataAccess/. ./KitchenRP.DataAccess
COPY ./KitchenRP.Web/. ./KitchenRP.Web
COPY ./KitchenRP.Domain/. ./KitchenRP.Domain
COPY ./KitchenRP.Test/. ./KitchenRP.Test

RUN dotnet publish -o out KitchenRP.Test

CMD ["dotnet", "test", "--logger:trx", "-r", "./test-results"]