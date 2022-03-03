FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY src/. ./src/
RUN dotnet restore
RUN dotnet build -c Release 