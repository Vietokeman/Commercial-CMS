# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["CMS.Api/CMS.Api.csproj", "CMS.Api/"]
COPY ["../CMS.Core/CMS.Core.csproj", "../CMS.Core/"]
COPY ["../CMS.Data/CMS.Data.csproj", "../CMS.Data/"]
RUN dotnet restore "CMS.Api/CMS.Api.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/CMS.Api"
RUN dotnet build "CMS.Api.csproj" -c Release -o /app/build

# Publish Stage
FROM build AS publish
RUN dotnet publish "CMS.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 5000

# Copy published app
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "CMS.Api.dll"]
