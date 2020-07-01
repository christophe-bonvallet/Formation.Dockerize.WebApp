FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build-env

ARG projectToPublish=./

########### COPY only file for restore ###########
WORKDIR /src

# We take sln and all csproj but in the same leval (root)
COPY ./*.sln */*.csproj ./

# move all csproj in the right directory (create it !)
RUN for file in $(ls *.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done
######### END COPY only file for restore #########

RUN dotnet restore
COPY . .

RUN dotnet publish $projectToPublish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine

ENV ASPNETCORE_ENVIRONMENT="Production"

WORKDIR /app
COPY --from=build-env /src/out .

# Run the generated shell script.
ENTRYPOINT ["./Formation.Dockerize.WebApp"]