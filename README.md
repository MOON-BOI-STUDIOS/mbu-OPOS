
# Top-Down Isometric 2D RPG with Solana Integration

Welcome to a unique top-down isometric 2D RPG, where players can explore a city map and engage in an immersive arcade room experience. This project stands out with its integration of the Solana blockchain, enabling players to upgrade in-game items using Bonk tokens.

![Game Screenshot](./screenshots/game.png)

## Table of Contents

- [Game Features](#game-features)
- [Technology Stack](#technology-stack)
- [Game Architecture](#game-architecture)
- [Installation & Setup](#installation--setup)
- [How to Play](#how-to-play)
- [Contributing](#contributing)
- [License](#license)

## Game Features

### 1. **MoonBikeRunner**
- Endless runner game where players travel on the y-axis, avoiding cars.
- Players can take boosts to travel faster and gain invincibility.
- Players can upgrade health, boost time, and bike look up to 5 levels with Bonk tokens.

### 2. **Battle Arena**
- Engage in sword battles with enemies.
- Upgrade swords and health to enhance combat abilities.

### 3. **Fishing Game**
- Enjoy a relaxing fishing experience with soothing music.
- Experience the joy of catching fish in a serene environment.

## Technology Stack

- **Unity Game Engine**: For creating the 2D RPG game environment and player interactions.
- **Magic Block SDK**: For integrating Solana blockchain to handle wallet logins and token transactions.
- **Solana Blockchain**: Handling secure and fast in-game token transactions.
- **Bonk Tokens**: In-game tokens used for upgrades.

## Game Architecture

This game follows a client-server architecture where:

- **Client Side**: Unity game engine handling player interactions, upgrades, and gameplay.
- **Server Side**: Solana blockchain for managing in-game token transactions.
- **Middleware**: Magic Block SDK facilitates communication between Unity and Solana blockchain.

For more details, refer to the [architecture diagram](./docs/architecture.png).

## Installation & Setup

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/MOON-BOI-STUDIOS/mbu-speedrun.git
   ```

2. **Open in Unity**:
   Open the project folder in Unity.

3. **Configure Wallet**:
   Follow the instructions in the game to log in using your Solana wallet.

4. **Play & Enjoy**:
   Explore the city and engage in the arcade room experience.

## How to Play

- **Log In**: Use your Solana wallet with a minimum of 0.5 SOL and 10 million Bonk tokens.
- **Explore**: Walk around the city and enter the arcade room.
- **Upgrade**: Stand in front of an arcade machine to upgrade using Bonk tokens.
- **Enjoy**: Play the games and have fun!

Distributed under the MIT License. See [LICENSE](./LICENSE) for more information.
