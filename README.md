# GTA V Style Game Prototype

This repository contains a basic prototype for an open-world action-adventure game similar to GTA V, built with Unity.

## Features Included

### 1. Third-Person Character Controller (`PlayerController.cs`)
- **WASD Movement**: Move relative to the camera angle.
- **Sprint**: Hold `Left Shift` to run faster.
- **Camera Follow**: Smooth rotation and movement tracking.
- **Physics**: Basic gravity and ground detection.

### 2. Arcade Vehicle Controller (`CarController.cs`)
- **Driving**: Use `W` to accelerate, `S` to brake/reverse.
- **Steering**: Use `A` and `D` to turn.
- **Physics**: Simulates acceleration, braking, drag, and steering angles.
- **Visuals**: Includes logic for rotating wheel meshes (requires setup).

## How to Use

### Prerequisites
- **Unity Hub & Editor**: Download from [unity.com](https://unity.com/).
- **IDE**: Visual Studio or VS Code with C# extensions.

### Setup Instructions

1. **Create a New Unity Project**
   - Select "3D" template.

2. **Import Scripts**
   - Drag `PlayerController.cs` and `CarController.cs` into your `Assets/Scripts` folder.

3. **Setup the Player**
   - Create a Capsule (`GameObject > 3D Object > Capsule`).
   - Add a `CharacterController` component.
   - Attach the `SimpleThirdPersonController` script.
   - Assign the Main Camera to the `Camera Transform` slot in the inspector.
   - *Tip*: Parent the Main Camera to an empty GameObject behind the player for better control.

4. **Setup a Car**
   - Create a Cube or import a Car model.
   - Add a `Rigidbody` component (ensure Mass is realistic, e.g., 1500).
   - Add `Box Colliders` for wheels/body if using a custom model.
   - Attach the `SimpleCarController` script.
   - Assign Wheel Transforms in the inspector for visual rotation.

5. **Create the World**
   - Add a Plane (`GameObject > 3D Object > Plane`) scaled up to act as the ground.
   - Add buildings, props, and roads to create a city environment.

## Controls

| Action | Key |
| :--- | :--- |
| Move | W, A, S, D |
| Sprint | Left Shift |
| Accelerate (Car) | W |
| Brake/Reverse (Car) | S |
| Steer (Car) | A, D |

## Next Steps for Development

To turn this into a full game like GTA V, you will need to add:
- **Enter/Exit Vehicles**: Raycasting to detect proximity and swapping control between Player and Car scripts.
- **AI Traffic**: NavMesh agents for pedestrians and other cars.
- **Combat System**: Raycast shooting or projectile physics.
- **Mission System**: A state machine to handle objectives.
- **World Streaming**: Loading/unloading chunks of the city dynamically.

## License
Free to use for learning and prototyping.
