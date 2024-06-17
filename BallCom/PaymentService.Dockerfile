# Use the official .NET Core SDK as a parent image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution and project files onto the container
COPY ./*.sln ./
COPY ./Shared/Shared.csproj ./Shared/
COPY ./PaymentService/PaymentService.csproj ./PaymentService/

# Restore the dependencies
RUN dotnet restore ./PaymentService/PaymentService.csproj

# Copy the rest of the application code
COPY ./PaymentService ./PaymentService
COPY ./Shared ./Shared

# Build and Publish the application
WORKDIR /src/PaymentService
RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

# Expose the port your application will run on
EXPOSE 3004

# Start the application
ENTRYPOINT ["dotnet", "PaymentService.dll"]