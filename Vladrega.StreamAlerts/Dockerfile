FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Vladrega.StreamAlerts/Vladrega.StreamAlerts.csproj", "Vladrega.StreamAlerts/"]
COPY ["Vladrega.StreamAlerts.Core/Vladrega.StreamAlerts.Core.csproj", "Vladrega.StreamAlerts/"]
COPY ["Vladrega.StreamAlerts.Connectors/Vladrega.StreamAlerts.Connectors.csproj", "Vladrega.StreamAlerts/"]

RUN dotnet restore "Vladrega.StreamAlerts/Vladrega.StreamAlerts.csproj"
COPY . .
WORKDIR "/src/Vladrega.StreamAlerts"


FROM build AS publish
RUN dotnet publish "Vladrega.StreamAlerts.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vladrega.StreamAlerts.dll"]
