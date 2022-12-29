FROM  ghcr.io/ffxivita/docker-dalamud:latest@sha256:ea3bc395211f1abc932d988c9c9be2729cd14e4b63b496525fbfd881a4031687

# Optout of telemtry.
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

# Create a non-root dev user.
RUN adduser --disabled-password --gecos "" dev

# Install required dependencies.
RUN apk add --update openssh

