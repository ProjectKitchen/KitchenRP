#!/usr/bin/env bash
cd docker
docker-compose -f docker-compose.yml up -d database
cd ../KitchenRP.Web/
ASPNETCORE_ENVIRONMENT=Development dotnet ef database update