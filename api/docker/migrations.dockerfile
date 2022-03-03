FROM timetracker-buildcontainer:latest AS build

# Copy everything else and build
WORKDIR /app/src/TimeTracker.SqlMigrations
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/src/TimeTracker.SqlMigrations/out .
ENTRYPOINT ["dotnet", "TimeTracker.SqlMigrations.dll"]
