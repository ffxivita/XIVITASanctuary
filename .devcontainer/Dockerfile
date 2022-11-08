# Immagine da usare
FROM ghcr.io/ffxivita/docker-dalamud:latest@sha256:e2b74777b1a3f018fbf7132069b26d8ca1647621f81c93aa593f87758d9e2480
    
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