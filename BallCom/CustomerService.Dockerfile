# Use the official .NET Core SDK as a parent image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the Shared class library project files
COPY ./Shared/Shared.csproj ./Shared/

# Copy the main project file and restore any dependencies
COPY ./CustomerService/CustomerService.csproj ./CustomerService/
RUN dotnet restore CustomerService/CustomerService.csproj

# Copy the rest of the application code
COPY ./CustomerService ./CustomerService
COPY ./Shared ./Shared

# Publish the application
WORKDIR /src/CustomerService
RUN dotnet publish -c Release -o /app/out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expose the port your application will run on
EXPOSE 3001

# Start the application
ENTRYPOINT ["dotnet", "CustomerService.dll"]