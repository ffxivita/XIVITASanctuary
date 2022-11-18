FROM  ghcr.io/ffxivita/docker-dalamud:latest@sha256:ca664625c7b457e6c9b406b15c09101b5178ea69044052ee06b824c7a71960d4

# Optout of telemtry.
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

# Create a non-root dev user.
RUN adduser --disabled-password --gecos "" dev

# Install required dependencies.
RUN apk add --update openssh

# Download the repository into /workspaces (overwrote if using a compliant tool).
WORKDIR /workspaces 
RUN git clone https://github.com/DarkArtek/XIVITASanctuary.git