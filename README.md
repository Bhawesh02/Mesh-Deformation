# Slime Slaughter: Wave Wars

Welcome to the repository for my game project, Slime Slaughter: Wave Wars! 🎮🔥

## Project Overview

Slime Slaughter: Wave Wars is an exhilarating game that thrusts players into a world teeming with slimes and challenges them to survive against relentless waves of enemies. With dynamic AI, immersive environments, and polished mechanics, this game promises an engaging and action-packed gaming experience.

## Features

- **Intense Battles:** Engage in heart-pounding battles against waves of slimes that progressively increase in difficulty. Adapt your strategy to overcome each new challenge.

- **Modular AI System:** The game features a sophisticated modular AI system for enemy behaviour. Each slime type employs different tactics, ensuring diverse and unpredictable encounters.

- **Strategic Environments:** Navigate through diverse environments with unique features. Narrow passages, obstacles, and interactive elements demand quick thinking and careful planning.

- **Responsive Controls:** Experience smooth and fluid controls that empower players to swiftly manoeuvre, attack, and dodge, contributing to a seamless and immersive gameplay experience.

- **Wave-Based Progression:** Survive through multiple waves, each more challenging than the last. Your survival skills will be put to the test as you strive to set new high scores.

## Coding Architecture

Slime Slaughter: Wave Wars incorporates a well-structured coding architecture and design patterns, enhancing the game's organization, performance, and gameplay.

### MVC (Model-View-Controller) Architecture

The game follows the MVC architecture to ensure a clear separation of responsibilities and maintainability. Both player and enemy entities have their own MVC structures, promoting modularity.

- **Player MVC:** The player's Model holds information about attributes like health, movement speed, and attack properties. The View manages rendering, animation, and UI interactions. The Controller processes user input and communicates between the Model and View.

- **Enemy MVC:** Similar to the player, enemy entities have their own Model, View, and Controller, encapsulating their behaviour and properties.

### Object Pooling

Object pooling is implemented for enemies that spawn in each wave. This optimization technique reduces memory overhead by reusing enemy objects, improving performance during intense gameplay.

### Singletons

Singleton design patterns are applied to specific systems that require global access:

- **GameManager:** Manages game state, wave progression, and overall game logic.
- **SoundService:** Controls in-game audio, providing an immersive auditory experience.
- **EventService:** Facilitates event handling, enabling communication between various game components.

### Player States

The player character has two states:

- **Idle State:** Represents the player when they are not moving. It transitions to the running state when the player starts moving.
- **Running State:** Activates when the player is moving. This state allows the player to move freely in all directions.

### Enemy States

Enemy AI is managed through three distinct states:

- **Idle State:** Represents the default state of an enemy, where it is stationary and observing its surroundings.
- **Chase State:** Activates when an enemy detects the player. The enemy dynamically follows the player's position while avoiding obstacles and other enemies which gives it a fluid movement.
- **Fight State:** This state triggers when an enemy enters the attack range of the player. The enemy engages in battle, implementing specific attack behaviours.


## GamePlay

[Video](https://www.loom.com/share/70ae88a73d8b445e933a51e54ebbf9b3)

### Screen Shots
![image](https://github.com/Bhawesh02/Slime-Slaughter-Wave-Wars/assets/93391124/d5f5631d-0ca7-4ad7-ace2-e2f238211a7a)
![image](https://github.com/Bhawesh02/Slime-Slaughter-Wave-Wars/assets/93391124/dce555b9-c215-4949-b11e-3efb8c5173dd)
![image](https://github.com/Bhawesh02/Slime-Slaughter-Wave-Wars/assets/93391124/e50f993b-78ed-4a36-944e-6ff701307cc0)



## Contributions

Although this project primarily represents my individual efforts, I'm open to feedback, suggestions, and discussions from the community. If you're passionate about game development and would like to share your insights or thoughts, feel free to connect!

## Contact

You can connect with me on LinkedIn: [Bhawesh Agarwal](https://www.linkedin.com/in/bhawesh-agarwal-70b98b113). Feel free to reach out if you're interested in discussing the game's mechanics, and development process, or if you simply want to talk about game design and development.

---
