# Come Contribuire
### Paragrafi
- [Localizzazione](#localizzazione)
- [Codice](#codice)
- [Scrittura Guide](#scrittura-guide)

---
## Localizzazione
La prima forma di contributo che puoi dare, sicuramente è una mano con la localizzazione delle guide. Tutti quelli che vorranno dare una mano, saranno inseriti nella pagina dei ringraziamenti dentro al plugin stesso.

### Tradurre i Duty
Le stringhe di localizzazione sono gestite da [Crowdin](https://crowdin.com/project/XIVITAGuide).

---

## Contribuire attraverso il codice
Quando si fanno dei cambiamenti al codice del plugin, assicurati di seguire le nostre linee guida e quelle di [Dalamud](https://goatcorp.github.io/faq/development#q-what-am-i-allowed-to-do-in-my-plugin). Ogni cambiamento che vìola queste regole, non verrà accettato.

### Compilazione
Per una maggiore compatibilità, si raccomanda l'uso del setup [DevContainer](./devcontainer) che è stato allegato a questo progetto. Si occuperà di installare tutte le dipendenze e configurare per te l'ambiente di sviluppo. Se vuoi invece sviluppare sulla tua macchina anzichè dentro al container, [puoi trovare una guida qui](https://plugins.ffxivita.it).
Se usi sistemi Unix (Linux etc), dovrai impostare manualmente la variabile `DALAMUD_HOME` dove effettivamente è la cartella (Questo lo fa il nostro DeVContainer per te, se scegli di usarlo)

### Standard di Compilazione
É fortemente consigliato di formattare il codice in modo appropriato in modo che sia leggibile e che possa essere esteso in futuro. La maggiorparte degli editor di codice dovrebbero fornire supporto di default senza installare una estensione aggiuntiva o un plugin.
Per quanto riguarda il codice in generale, non ci sono regole ferree da seguire ma seguire le indicazioni standard di CSharp è altamente raccomandato.

### Funzionalità
Assicurati che il codice sia propriamente separato in base alle sua funzionalità. Ad esempio gli elementi di UI non dovrebbero contenenere logiche che non vengono usate per il disegno della stessa o visualizzare informazioni. Il recupero delle informazioni dovrebbe essere chiamato da una classe differente e da un file differente.

### Documentazione & Commenti
Si prega di inserire `<summary>` per ogni funzione, metodo e classe; in modo da capire le intenzioni dietro al codice e aiutare gli altri a capire come usarlo quando lavorano al plugin.

### UpdateManager
Cerca di non fare cambiamenti alla classe UpdateManager in quanto è una classe critica per pubblicare aggiornamenti e visto che li "pesca" da una fonte esterna, è meglio non toccare questa classe. Comunque, se ritieni di poter migliorarla, sei il/la benvenuto/a.

---

## Contribuire con le Guide
Il contributo con le guide è molto apprezzato visto la quantità di duty preseni in game. Dovremmo aver già creato tutti i files per tutti i duty esistenti e ovviamente ne verranno aggiunti di nuovi ogni qual volta verranno aggiunti al gioco. 

### Creare le guide
Le guide sono conservate in formato `.json` dentro il seguente percorso `Resources/Localization/Duty/<lang>/` e sono chiamate col rispettivo nome Inglese, per maggiore facilità.
Quando si crea un file, è importante passare prima da un sistema di validazione di file Json prima di fare il commit. Altrimenti non verrà caricato dal plugin e non sarà visibile.
L'editor di guide incorporato a questo plugin, ha un sistema di validazione json incorporato e permette anche di vedere una anteprima delle modifiche che andate a fare. Potete accedere all'editor col comando `/xivita-editor` se avete il plugin installato. 

##### Chiavi per i file JSON
- `Version`: Versione del file duty, non cambiare a mano.
- `Name`: Nome del Duty
- `Difficulty`: ID della difficoltà del duty
- `Type`: ID del duty
- `Expansion`: ID della espansione
- `Level`: Livello del duty
- `UnlockQuestID`: [ID della quest](https://github.com/xivapi/ffxiv-datamining/blob/master/csv/Quest.csv) che sblocca il duty
- `TerritoryID`: ID del territorio del duty quando si è dentro
- `Bosses`: Lista dei Boss
    - `Name`: Nome del Boss
    - `Strategy` (si preferisce): Una guida testuale completa su come battere il boss
    - `TLDR` (opzionale): Una guida per chi ha fretta su come battere il boss
    - `KeyMechanics` (opzionale): Una lista delle meccaniche
      - `Name`: Nome della meccanicha
      - `Description`: Descrizione della meccanica
      - `Type`: ID della meccanica

Un esempio di Duty, può essere trovato [qui](src/Resources/Localization/Duty/en/A%20Realm%20Reborn/Dungeons/CopperbellMines.json).

Quando scrivi una descrizione delle meccaniche del boss, cerca sempre di essere il più conciso possibile. 

#### Formattazione
Quando si scrive una guida, si può usare `\n` per andare a capo e creare una nuova linea. Puoi usare `\t` per rientrare di 1 tab quando hai bisogno. In caso di percentuale, usare `%%` per visualizzare il segno di percentuale.

#### ID Interni (Type, Difficulty etc.)
Tutti gli ID sono conservati dentro la [cartella enums](src/Enums/) o sono visibili dentro l'editor in-game.

### Altri ID (Quest, Territory, etc)
Tutti gli altri ID sono conservati in files csv dentro al repository [FFXIV-Datamining](https://github.com/xivapi/ffxiv-datamining)

---

