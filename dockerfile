# Image de base aspnet core 3.1
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine

# https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-3.1
ENV ASPNETCORE_ENVIRONMENT Production

COPY ./Formation.Dockerize.WebApp/bin/Release/netcoreapp3.1/publish/ webapp 


WORKDIR /webapp

ENTRYPOINT ["dotnet", "Formation.Dockerize.WebApp.dll"]