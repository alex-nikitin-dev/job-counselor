# ---------------------------------------------------------------
# Dockerfile for building the JobCounselor WebApi project.
# This uses a multi-stage build to create a self-contained
# executable targeting .NET 8 running on Linux.
# ---------------------------------------------------------------

# --- Build stage ------------------------------------------------
# Use the .NET 8 SDK image to restore packages and publish the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /src

# Copy the entire repository into the build container
COPY . .

# Restore NuGet packages for the WebApi project
RUN dotnet restore src/WebApi/WebApi.csproj

# Publish the WebApi as a self-contained application for linux-x64
RUN dotnet publish src/WebApi/WebApi.csproj \
    -c Release \
    -o /app/publish \
    -p:PublishSingleFile=true \
    -p:PublishTrimmed=false \
    -p:SelfContained=true \
    -r linux-x64 \
    -p:IncludeNativeLibrariesForSelfExtract=true

# --- Runtime stage ----------------------------------------------
# Use the lightweight runtime-deps image since the app is self-contained
FROM mcr.microsoft.com/dotnet/runtime-deps:8.0 AS runtime

# Expose the port the application listens on
EXPOSE 8080

# Set the working directory
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Run the application. The executable name matches the project name
ENTRYPOINT ["./WebApi"]

