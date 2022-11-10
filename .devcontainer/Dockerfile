# Immagine da usare
FROM ghcr.io/ffxivita/docker-dalamud:latest@sha256:b40ba1ce7e423242e71225c401ed0b1ccedf11e1cff8cb6edd735e8a0b3afa7c
    
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