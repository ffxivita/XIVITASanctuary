FROM  ghcr.io/ffxivita/docker-dalamud:latest@sha256:de85bc04e2c9357db69c109379439cb8ce5b91612307ea93d08c1fedd9dd4c22

# Optout of telemtry.
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

# Create a non-root dev user.
RUN adduser --disabled-password --gecos "" dev

# Install required dependencies.
RUN apk add --update openssh

