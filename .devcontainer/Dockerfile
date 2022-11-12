# Immagine da usare
FROM ghcr.io/ffxivita/docker-dalamud:latest@sha256:eb88c800c6f6df456f5e89a35e23f5166e9d24c055b3a83676865c6acff333bf
    
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