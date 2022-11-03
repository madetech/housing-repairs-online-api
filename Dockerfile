FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
RUN apt-get update && apt-get install -y postgresql-client
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HousingRepairsOnlineApi/HousingRepairsOnlineApi.csproj", "HousingRepairsOnlineApi/"]
COPY . .
WORKDIR "/src/HousingRepairsOnlineApi"
RUN dotnet build "HousingRepairsOnlineApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HousingRepairsOnlineApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HousingRepairsOnlineApi.dll"]
