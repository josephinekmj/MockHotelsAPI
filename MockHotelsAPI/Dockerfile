# Base image (Linux)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Build image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["MockHotelsAPI.csproj", "./"]
RUN dotnet restore "MockHotelsAPI.csproj"
COPY . .
RUN dotnet publish "MockHotelsAPI.csproj" -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MockHotelsAPI.dll"]
