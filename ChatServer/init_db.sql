-- 🧱 Création des tables de base pour ton chat

-- Table des utilisateurs (Identity simplifiée)
CREATE TABLE IF NOT EXISTS AspNetUsers (
    Id TEXT PRIMARY KEY,
    UserName TEXT NOT NULL UNIQUE,
    Email TEXT,
    PasswordHash TEXT,
    DisplayName TEXT,
    IsOnline BOOLEAN DEFAULT 0
);

-- Table des conversations (groupe ou privée)
CREATE TABLE IF NOT EXISTS Conversations (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Table des messages
CREATE TABLE IF NOT EXISTS Messages (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Content TEXT NOT NULL,
    SentAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    SenderId TEXT NOT NULL,
    ConversationId INTEGER NOT NULL,
    FOREIGN KEY (SenderId) REFERENCES AspNetUsers(Id),
    FOREIGN KEY (ConversationId) REFERENCES Conversations(Id)
);

-- Table des fichiers attachés
CREATE TABLE IF NOT EXISTS FileAttachments (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    FileName TEXT NOT NULL,
    StoredFileName TEXT NOT NULL,
    ContentType TEXT NOT NULL,
    MessageId INTEGER NOT NULL,
    FOREIGN KEY (MessageId) REFERENCES Messages(Id)
);

-- Table de liaison utilisateur <-> conversation
CREATE TABLE IF NOT EXISTS UserConversations (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    ConversationId INTEGER NOT NULL,
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id),
    FOREIGN KEY (ConversationId) REFERENCES Conversations(Id)
);
