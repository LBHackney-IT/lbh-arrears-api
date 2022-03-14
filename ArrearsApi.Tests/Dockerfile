FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# disable microsoft telematry
ENV DOTNET_CLI_TELEMETRY_OPTOUT='true'

ARG LBHPACKAGESTOKEN
ENV LBHPACKAGESTOKEN=$LBHPACKAGESTOKEN

WORKDIR /app

# Copy csproj and nuget config and restore as distinct layers
COPY ./ArrearsApi.sln ./
COPY ./ArrearsApi/ArrearsApi.csproj ./ArrearsApi/
COPY ./ArrearsApi.Tests/ArrearsApi.Tests.csproj ./ArrearsApi.Tests/
COPY /NuGet.Config /root/.nuget/NuGet/NuGet.Config

RUN dotnet restore ./ArrearsApi/ArrearsApi.csproj
RUN dotnet restore ./ArrearsApi.Tests/ArrearsApi.Tests.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c debug -o out ArrearsApi.Tests/ArrearsApi.Tests.csproj

CMD dotnet test
