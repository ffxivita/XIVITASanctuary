FROM  ghcr.io/ffxivita/docker-dalamud:latest@sha256:f2700f00fd40e4fd69356b4c4ddd4f2b62a58ad612a98fc8be9e6e83799fdaf7

# Optout of telemtry.
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

# Create a non-root dev user.
RUN adduser --disabled-password --gecos "" dev

# Install required dependencies.
RUN apk add --update openssh

