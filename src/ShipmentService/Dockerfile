FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy everything
COPY . .
# Restore as distinct layers
RUN dotnet restore "./ShipmentService/Shipment.Grpc"
# Build and publish a release
RUN dotnet publish "./ShipmentService/Shipment.Grpc/Shipment.Grpc.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Shipment.Grpc.dll"]