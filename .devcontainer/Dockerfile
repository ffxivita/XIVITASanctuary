FROM  ghcr.io/ffxivita/docker-dalamud:latest@sha256:280c5a93b696e08eef2601c1b55bc256c29441235cef31ecb9423ec28a2e41a3

# Optout of telemtry.
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

# Create a non-root dev user.
RUN adduser --disabled-password --gecos "" dev

# Install required dependencies.
RUN apk add --update openssh

