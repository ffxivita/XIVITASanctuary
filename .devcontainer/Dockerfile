# Immagine da usare
FROM ghcr.io/ffxivita/docker-dalamud:latest@sha256:05830c36fba15c4c784394ef5980c19f751d967c6d8385821c70a0143fb20370
    
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