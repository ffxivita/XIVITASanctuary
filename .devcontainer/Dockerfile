# Immagine da usare
FROM ghcr.io/ffxivita/docker-dalamud:latest@sha256:7eac15f19c7c87a1e82ec50fb3f751ea563932404679c37eb378fdc2f01bac78
    
# Variabili d'Ambiente
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1
    
# Crea un utente non-root.
RUN adduser --disabled-password --gecos "" dev
# Installa le dipendenze.
RUN apk add --update openssh
    
# Clona il repository dentro /workspaces (sovvrascrive se già presente).
WORKDIR /workspaces
RUN git clone https://github.com/ffxivita/XIVITAGuide.git