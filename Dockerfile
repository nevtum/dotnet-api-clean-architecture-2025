# Use the .NET SDK image for the build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy csproj files and restore as distinct layers
COPY src/Project.slnx ./
COPY src/api/*.csproj ./api/
COPY src/common/*.csproj ./common/
RUN dotnet restore

# Copy the entire app
COPY src ./

# Build the application
RUN dotnet publish api -c Release -o out

# Use the ASP.NET runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0

RUN groupadd -r appuser && useradd -r -g appuser appuser

WORKDIR /app
COPY --from=build --chown=appuser:appuser /app/out .

USER appuser

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80

# Expose the port the app runs on
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "api.dll"]
