FROM  ghcr.io/ffxivita/docker-dalamud:latest@sha256:a18464a9c8771d5d15fcd9d08b448755a644d2cdb7c17e070d657909bc682902

# Optout of telemtry.
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

# Create a non-root dev user.
RUN adduser --disabled-password --gecos "" dev

# Install required dependencies.
RUN apk add --update openssh

