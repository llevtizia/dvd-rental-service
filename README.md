# DVD Rental Service

Applicazione web per la gestione del noleggio di DVD di un negozio: catalogo con disponibilità, clienti e registrazione noleggi/restituzioni.

## Stack scelto

- **Backend**: ASP.NET Core (C#) + Entity Framework Core + SQLite
- **Frontend**: React (Vite)
- **API docs**: Scalar (OpenAPI UI) su `/scalar/v1`, disponibile solo in ambiente di sviluppo

## Avvio backend

```bash
cd dvd-rental               # cartella del progetto .NET
dotnet restore              # scarica le dipendenze NuGet
dotnet ef database update   # crea il database SQLite e semina il catalogo DVD
dotnet run
```

L'API sarà disponibile sulla porta indicata in console all'avvio (es. `http://localhost:5114`, configurabile in `Properties/launchSettings.json`). La documentazione interattiva è su `http://localhost:<port>/scalar/v1`.

## Avvio frontend

```bash
cd dvd-rental-client
npm install   # solo la prima volta, per scaricare le dipendenze
npm run dev
```
L'app sarà disponibile sull'URL mostrato in console (di norma http://localhost:5173). **Nota**: se il frontend non parte su http://localhost:5173 (perché la porta è occupata), è necessario aggiornare la configurazione CORS in `Program.cs` (WithOrigins) con la porta effettiva mostrata da Vite in console.

**Importante**: avviare backend e frontend contemporaneamente da due terminali separati.

## Seed dei dati

Il catalogo DVD viene popolato automaticamente al primo avvio tramite `UseSeeding`/`UseAsyncSeeding` configurati in `Program.cs`, senza alcuna azione manuale.

I clienti, al contrario, sono creati tramite l'endpoint `POST /api/Clienti`, come richiesto dalla traccia ("creato e gestito via API").

## Endpoint principali

| Metodo | Route | Descrizione |
|---|---|---|
| GET | `/api/Clienti` | Elenco clienti |
| GET | `/api/Clienti/{id}` | Dettaglio cliente |
| POST | `/api/Clienti` | Crea un nuovo cliente |
| GET | `/api/Dvd` | Catalogo DVD con copie disponibili |
| POST | `/api/Noleggi` | Crea un noleggio (verifica disponibilità) |
| GET | `/api/Noleggi/{id}` | Dettaglio noleggio |
| GET | `/api/Noleggi?clienteId=&titolo=` | Storico noleggi, filtrabile per cliente e/o titolo |
| PUT | `/api/Noleggi/{id}/restituzione` | Chiude un noleggio (restituzione) |



## Assunzioni e scelte progettuali

- **Disponibilità DVD calcolata a runtime**: il numero di copie disponibili di un DVD non è salvato come campo, ma calcolato come `QuantitaTotale - noleggi attivi (RestituzioneEffettiva == null)`, per evitare disallineamenti tra dato salvato e stato reale.
- **Database SQLite**: scelto per un setup più semplice in locale (nessun server esterno da installare, scalabilità multi utente non richiesta), mantenendo comunque la persistenza dei dati.
- **Validazioni di esistenza**: la creazione di un noleggio verifica esplicitamente che sia il cliente sia il DVD esistano, restituendo `404` altrimenti; se non ci sono copie disponibili, restituisce `400`.
- **Nessun repository/service layer separato**: date le dimensioni ridotte del progetto, la logica di business resta nei controller con accesso diretto a `DbContext` per evitare complessità non necessarie.
- **CORS** è configurato per accettare richieste solo da http://localhost:5173 (l'origine del frontend in sviluppo).


## Funzionalità implementate


- Gestione clienti (creazione, elenco)
- Catalogo DVD con disponibilità in tempo reale
- Creazione noleggi con controllo automatico delle copie disponibili
- Restituzione DVD
- Storico noleggi con filtri per cliente e titolo
