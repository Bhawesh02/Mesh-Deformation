# Mesh Deformer Tool

The Mesh Deformer Tool is a Unity-based tool designed to modify and manipulate mesh geometry dynamically. Using deformation mechanics, this tool empowers developers and artists to create interactive environments and effects by altering mesh structures directly in the scene during **Play, edit or in timeline mode**.

## Features

- **Dynamic Mesh Deformation:**

  - Real-time mesh manipulation using **raycasting**.
  - Multiple deformation modes:
    - **Deform Mode:** Modify mesh surfaces interactively.
    - **Uniform Mode:** Reset mesh to its original state effortlessly.

- **Customizable Deformation Settings**

  - Fine-tune **deformation height, radius, and smoothness** using **Animation Curves**.
  - Adjustable deformation axis for precise control.

- **Editor & Timeline Support**

  - **Edit Mode:** Apply deformations directly in the Unity Editor for static setups.
  - **Timeline Integration:** Enable dynamic deformation animations within Unity's Timeline.'

- **Custom Mesh Generation**
  - Dynamically create grid-based meshes with customizable dimensions and resolution for procedural effects or gameplay prototyping.

## Coding Architecture

### Core Components

- `MeshDeformReceiver`
  - Handles deformation logic and applies transformations to mesh vertices.
  - Supports deformation along multiple axes (e.g., X, Y, Z, and negative counterparts).
- `MeshDeformer`
  - Implements raycasting logic to detect mesh objects and apply deformation.
  - Includes states for deforming and resetting meshes.
- `MeshGeneration`
  - Dynamically generates polygonal grid meshes based on dimensions and resolution set by the user.

### Scriptable Object for Data Management

- `MeshDeformerData`
  - Centralized storage for deformation parameters like height, radius, smoothness curves, and layer masks.
  - Simplifies reuse and consistency across multiple mesh objects.

### Custom Editors

- `MeshDeformReceiverEditor`
  - Adds an Editor button to reset the mesh directly within the Inspector.
- `MeshDeformerEditor`
  - Provides intuitive scene view tools for configuring raycast endpoints and deformation parameters.
  - Assigns deformation data efficiently in the Unity Inspector.

## How It Works

<ol>
    <li>Attach the MeshDeformReceiver component to a mesh object.</li>
    <li>Add MeshDeformer to define raycasting and deformation parameters.</li>
    <li>Configure deformation settings (axis, height, radius, and smoothness) in the Inspector.</li>
    <li>Configure deformation settings (axis, height, radius, and smoothness) in the Inspector.</li>
    <li>Trigger deformation in <b>real-time</b> or<b> during edit/timeline mode.</b></li>
    <li>Reset the mesh to its original state at any time.</b></li>
</ol>

## Gameplay Use Cases

- Terrain Modification:

  - Create interactive terrains with dynamic features like footprints, craters, or custom paths.

- Interactive Objects:

  - Add realism by enabling objects to deform during gameplay, such as dented surfaces or reactive environments.

- Procedural Effects:
  - Generate and manipulate procedural meshes with specific patterns, making prototyping faster and more versatile.

## Working video

[![](https://github.com/Bhawesh02/Mesh-Deformation/blob/master/Mesh%20Deform%20Test/Assets/Extra/Mesh%20Deformer.gif)](https://youtu.be/mKKhAUzzRZM)

## Contributions

This tool represents my individual development efforts but thrives on community engagement. Feedback, suggestions, and collaborative contributions are highly encouraged. If you're passionate about mesh deformation techniques, Unity development workflows, or procedural generation, I’d love to connect!

## Contact

You can connect with me on LinkedIn: [Bhawesh Agarwal](https://www.linkedin.com/in/bhawesh-agarwal-70b98b113). Feel free to reach out if you're interested in discussing the game's mechanics, and development process, or if you simply want to talk about game design and development.

---
