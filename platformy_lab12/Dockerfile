FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["platformy_lab12/platformy_lab12.csproj", "platformy_lab12/"]
RUN dotnet restore "platformy_lab12/platformy_lab12.csproj"
COPY . .
WORKDIR "/src/platformy_lab12"
RUN dotnet build "platformy_lab12.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "platformy_lab12.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "platformy_lab12.dll"]
