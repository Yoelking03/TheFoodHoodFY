# ---------- etapa 1 : build ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["TheFoodHood.sln", "./"]
COPY ["TheFoodHood.Web/TheFoodHood.Web.csproj", "TheFoodHood.Web/"]

RUN dotnet restore "TheFoodHood.sln"

COPY . .

RUN dotnet publish "TheFoodHood.Web/TheFoodHood.Web.csproj" \
    -c Release -o /app/publish \
    /p:PublishTrimmed=true \
    /p:EnableCompressionInSingleFile=true

# ---------- etapa 2 : runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TheFoodHood.Web.dll"]
