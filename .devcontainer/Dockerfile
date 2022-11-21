FROM  ghcr.io/ffxivita/docker-dalamud:latest@sha256:0c322e40954614ee0a326751490ac960c0ccd8884c0db79c84017e3465fb1165

# Optout of telemtry.
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

# Create a non-root dev user.
RUN adduser --disabled-password --gecos "" dev

# Install required dependencies.
RUN apk add --update openssh

