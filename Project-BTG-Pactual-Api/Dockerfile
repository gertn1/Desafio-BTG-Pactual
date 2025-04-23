# Fase base: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Fase de build: SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# 1) copie só o csproj e restaure pacotes
COPY ["Project-BTG-Pactual-Api.csproj", "./"]
RUN dotnet restore "./Project-BTG-Pactual-Api.csproj"

# 2) copie todo o restante do projeto e compile
COPY . .
RUN dotnet build "./Project-BTG-Pactual-Api.csproj" \
    -c $BUILD_CONFIGURATION -o /app/build

# Fase de publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Project-BTG-Pactual-Api.csproj" \
    -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase final: runtime com binário publicado
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Project-BTG-Pactual-Api.dll"]
