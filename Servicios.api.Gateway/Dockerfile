#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Servicios.api.Gateway/Servicios.api.Gateway.csproj", "Servicios.api.Gateway/"]
RUN dotnet restore "Servicios.api.Gateway/Servicios.api.Gateway.csproj"
COPY . .
WORKDIR "/src/Servicios.api.Gateway"
RUN dotnet build "Servicios.api.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Servicios.api.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Servicios.api.Gateway.dll"]