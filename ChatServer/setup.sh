#!/bin/bash
# Script d’installation et de configuration pour ChatServer (.NET 8 + SQLite)
# Compatible Linux (Kali, Ubuntu, etc.)



echo "📥 Installation des dépendances nécessaires..."

# Frameworks principaux
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.11
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.11
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.11

# Gestion des utilisateurs et rôles
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 8.0.11

# SignalR (communication temps réel)
dotnet add package Microsoft.AspNetCore.SignalR.Core --version 8.0.11

# JWT Authentification
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.11
dotnet add package System.IdentityModel.Tokens.Jwt --version 7.1.2

# Swagger (documentation API)
dotnet add package Swashbuckle.AspNetCore --version 6.6.2
dotnet add package Microsoft.AspNetCore.OpenApi --version 8.0.21

# Logger
dotnet add package Serilog.AspNetCore --version 8.0.0
dotnet add package Serilog.Sinks.File --version 5.0.0

# Sqlite
dotnet add package Microsoft.Data.Sqlite


echo "🔄 Restauration des dépendances..."
dotnet restore


