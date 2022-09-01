FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HousingRepairsOnlineApi/HousingRepairsOnlineApi.csproj", "HousingRepairsOnlineApi/"]
ARG USERNAME
ARG TOKEN
RUN dotnet nuget add source --username $USERNAME --password $TOKEN --store-password-in-clear-text --name github "https://nuget.pkg.github.com/City-of-Lincoln-Council/index.json"
RUN dotnet restore "HousingRepairsOnlineApi/HousingRepairsOnlineApi.csproj"
COPY . .
WORKDIR "/src/HousingRepairsOnlineApi"
RUN dotnet build "HousingRepairsOnlineApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HousingRepairsOnlineApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HousingRepairsOnlineApi.dll"]
