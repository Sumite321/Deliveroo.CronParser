FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Deliveroo.CronParser/Deliveroo.CronParser.csproj", "Deliveroo.CronParser/"]
RUN dotnet restore "Deliveroo.CronParser/Deliveroo.CronParser.csproj"
COPY . .
WORKDIR "/src/Deliveroo.CronParser"
RUN dotnet build "Deliveroo.CronParser.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Deliveroo.CronParser.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Deliveroo.CronParser.dll"]
