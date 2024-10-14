# Base image for .NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Build stage for backend
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the .csproj file for backend
COPY dahliav2.Server/dahliav2.Server.csproj ./dahliav2.Server/

# Restore backend dependencies
RUN dotnet restore "dahliav2.Server/dahliav2.Server.csproj"

# Copy the entire backend source code
COPY dahliav2.Server/ ./dahliav2.Server/

# Build the backend application
RUN dotnet build "dahliav2.Server/dahliav2.Server.csproj" -c Release -o /app/build

# Build stage for frontend (Vite)
FROM node:18 AS frontend-build
WORKDIR /app

# Copy the package.json and lock file for the frontend
COPY dahliav2.client/package.json ./dahliav2.client/package-lock.json ./dahliav2.client/tsconfig.app.json ./

# Install frontend dependencies
RUN npm install

# Copy the entire frontend source code
COPY dahliav2.client/ .

# Build the frontend (Vite)
RUN npm run build

# Publish the backend and prepare static files
FROM build AS publish
RUN dotnet publish "dahliav2.Server/dahliav2.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage - serve the backend and frontend together
FROM base AS final
WORKDIR /app

# Copy the published backend
COPY --from=publish /app/publish .

# Copy the built frontend files to the backend wwwroot
COPY --from=frontend-build /app/dist ./wwwroot

# Entry point for the .NET backend
ENTRYPOINT ["dotnet", "dahliav2.Server.dll"]