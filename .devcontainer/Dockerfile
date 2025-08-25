FROM mcr.microsoft.com/devcontainers/dotnet:9.0 AS base

# Install EF Core CLI tools

RUN dotnet tool install --tool-path /home/vscode/.dotnet/tools dotnet-ef

ENV PATH="${PATH}:/home/vscode/.dotnet/tools"

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_24.x | bash - \
    && apt-get install -y nodejs