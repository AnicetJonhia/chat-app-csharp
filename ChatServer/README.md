# ChatApp Backend (ASP.NET Core)

Ce projet est un **backend pour une application de chat** simple utilisant ASP.NET Core, SignalR et SQLite avec authentification JWT.

---

## Technologies utilisées

- **.NET 8 / ASP.NET Core**
- **Entity Framework Core** avec SQLite
- **ASP.NET Core Identity** pour la gestion des utilisateurs
- **JWT (JSON Web Token)** pour l’authentification
- **SignalR** pour le chat en temps réel
- **Swagger / OpenAPI** pour tester les API

---

## Fonctionnalités

### Authentification

- **Register** : `POST /api/Account/register`  
- **Login** : `POST /api/Account/login` (retourne un token JWT)

### Gestion des messages

- **Envoyer un message** : `POST /api/Messages/{conversationId}`  
- **Historique des messages** : `GET /api/Messages/history/{conversationId}`

### Gestion des fichiers

- **Upload d’un fichier** : `POST /api/Files/upload/{messageId}`  
- **Téléchargement d’un fichier** : `GET /api/Files/download/{id}`

### Temps réel

- **SignalR Hub** : `/chathub` pour l’échange de messages instantanés

---

## Configuration

Editer `appsettings.json` et remplir la section `Jwt` :

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
```

``` bash
cd chatserver/
chmod +x setup.sh
./setup.sh
dotnet ef migrations add InitialCreate
dotnet run
```

L’API sera disponible sur http://localhost:5113