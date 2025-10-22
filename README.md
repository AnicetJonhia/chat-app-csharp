# ChatApp Backend (ASP.NET Core)

Backend pour une application de chat simple avec ASP.NET Core, SignalR et SQLite, utilisant JWT pour l'authentification.

---

## Technologies utilisées

- .NET 8 / ASP.NET Core
- Entity Framework Core avec SQLite
- ASP.NET Core Identity
- JWT (JSON Web Token)
- SignalR pour le chat en temps réel
- Swagger / OpenAPI pour tester les API

---

## Endpoints

### Authentification

- **Register** : `POST /api/Account/register`  
- **Login** : `POST /api/Account/login` (retourne un token JWT)

### Conversations

- **Créer une conversation** : `POST /api/Conversations/create`  
- **Mes conversations** : `GET /api/Conversations/my`  

### Messages

- **Envoyer un message** : `POST /api/Messages/{conversationId}`  
- **Historique des messages** : `GET /api/Messages/history/{conversationId}`

### Fichiers

- **Upload** : `POST /api/Files/upload/{messageId}`  
- **Téléchargement** : `GET /api/Files/download/{id}`

### Temps réel

- **SignalR Hub** : `/chathub` pour les échanges instantanés

---

## Configuration

Éditer `appsettings.json` et remplir la section `Jwt` :

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=chatapp.db"
  },
  "Jwt": {
    "Key": "VOTRE_CLE_SECRETE",
    "Issuer": "ChatApp",
    "Audience": "ChatAppUsers",
    "ExpireMinutes": "60"
  }
}

Lancer le projet
``` bash
cd ChatServer/
chmod +x setup.sh
./setup.sh
dotnet ef migrations add InitialCreate
dotnet run

```

L’API sera disponible sur :
``` bash
 http://localhost:5113
```