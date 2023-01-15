#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WarehouseService.Api/WarehouseService.Api.csproj", "WarehouseService.Api/"]
RUN dotnet restore "WarehouseService.Api/WarehouseService.Api.csproj"
COPY . .
WORKDIR "/src/WarehouseService.Api"
RUN dotnet build "WarehouseService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WarehouseService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WarehouseService.Api.dll"]