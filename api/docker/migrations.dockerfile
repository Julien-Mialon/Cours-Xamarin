FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY NuGet.Config ./
COPY src/. ./src/
RUN dotnet restore

# Copy everything else and build
WORKDIR /app/src/TD.SqlMigrations
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/src/TD.SqlMigrations/out .
COPY docker/wait-for-it.sh . 
COPY docker/wait-and-run.sh .
ENTRYPOINT ["./wait-and-run.sh", "dotnet", "TD.SqlMigrations.dll", "docker"]
