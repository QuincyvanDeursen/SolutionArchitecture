﻿# Use the official .NET Core SDK as a parent image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution and project files onto the container
COPY ./*.sln ./
COPY ./Shared/Shared.csproj ./Shared/
COPY ./InventoryManagement/InventoryManagement.csproj ./InventoryManagement/

# Restore the dependencies
RUN dotnet restore ./InventoryManagement/InventoryManagement.csproj

# Copy the rest of the application code
COPY ./InventoryManagement ./InventoryManagement
COPY ./Shared ./Shared

# Build and Publish the application
WORKDIR /src/InventoryManagement
RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

# Expose the port your application will run on
EXPOSE 3002

# Start the application
ENTRYPOINT ["dotnet", "InventoryManagement.dll"]