FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MoneyTracker2.0/MoneyTracker2.0.csproj", "MoneyTracker2.0/"]
RUN dotnet restore "MoneyTracker2.0/MoneyTracker2.0.csproj"
COPY . .
WORKDIR "/src/MoneyTracker2.0"
RUN dotnet build "MoneyTracker2.0.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MoneyTracker2.0.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MoneyTracker2.0.dll"]
