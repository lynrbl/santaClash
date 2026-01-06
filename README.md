# Santa Clash

Un jeu coopératif développé avec **MonoGame** où deux joueurs doivent protéger le Père Noël des billets de banque !

## Lancer le jeu

### Prérequis
- .NET 9.0 SDK
- MonoGame 3.8+

### Exécution
```bash
cd santaClash
dotnet run
```

Ou ouvrir `santaClash.sln` dans Visual Studio et appuyer sur **F5**.

## Commandes

### Joueur 1 (Leprechaun gauche)

| Action | Clavier | Manette |
|--------|---------|---------|
| Haut | W | Joystick gauche ↑ / D-Pad ↑ |
| Bas | S | Joystick gauche ↓ / D-Pad ↓ |
| Gauche | A | Joystick gauche ← / D-Pad ← |
| Droite | D | Joystick gauche → / D-Pad → |

### Joueur 2 (Leprechaun droit)

| Action | Clavier | Manette |
|--------|---------|---------|
| Haut | ↑ | Joystick gauche ↑ / D-Pad ↑ |
| Bas | ↓ | Joystick gauche ↓ / D-Pad ↓ |
| Gauche | ← | Joystick gauche ← / D-Pad ← |
| Droite | → | Joystick gauche → / D-Pad → |

### Commandes générales

| Action | Clavier | Manette |
|--------|---------|---------|
| Quitter | Échap | Back |
| Recommencer (Game Over) | R | Start / A |

## Fonctionnalités

- **Mode 2 joueurs** : Jouez en coopération avec un ami
- **Support manettes** : Compatible avec les manettes Xbox/compatibles XInput
- **Contrôles mixtes** : Clavier et manette fonctionnent simultanément
- **Vagues d'ennemis** : Les billets apparaissent par vagues depuis les bords de l'écran
- **Système de score** : Chaque joueur a son propre compteur de billets interceptés
- **Barre d'argent** : Le Père Noël a une jauge qui se remplit quand les billets l'atteignent
- **Game Over** : La partie se termine quand la barre d'argent est pleine

## But du jeu

Empêchez les billets de banque d'atteindre le Père Noël ! Interceptez-les avec vos leprechauns avant qu'ils ne remplissent sa barre d'argent.

## Structure du projet

```
santaClash/
├── Game1.cs        # Logique principale du jeu
├── Player.cs       # Classe des joueurs (Leprechauns)
├── Santa.cs        # Classe du Père Noël
├── Enemy.cs        # Classe des ennemis (Billets)
├── GameObject.cs   # Classe de base
└── Content/        # Assets (textures, fonts)
```

---
