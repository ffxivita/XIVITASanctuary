FROM  ghcr.io/ffxivita/docker-dalamud:latest@sha256:35273491ae387da9d4a1066feacd33db58cb547d5efcfbf8504d4db5596e6f45

# Optout of telemtry.
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

# Create a non-root dev user.
RUN adduser --disabled-password --gecos "" dev

# Install required dependencies.
RUN apk add --update openssh

