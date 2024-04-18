![GitHub-Mark-Light](https://user-images.githubusercontent.com/50978201/193459338-32d71599-19d6-4eb6-b5b3-c34348d623b9.svg#gh-dark-mode-only)![GitHub-Mark-Dark](https://user-images.githubusercontent.com/50978201/193459322-b078ed0d-cf0d-4791-ad10-ee2f3131cd20.svg#gh-light-mode-only)

<p align="center">
    Final Engine is an open-source, cross-platform Game Engine developed in C# 11.0, using .NET 7.0. What began as a hobby project has rapidly evolved into a project that I'm committed to maintaining and developing actively over time. The primary objective of Final Engine is to offer a feature-rich Game Engine that prioritizes simplicity and accessibility for new users. Our mission statement:
</p>

<p align="center">
    <em>Create an engine that makes game development enjoyable, straightforward, and effortless while granting users complete creative freedom.</em>
</p>

<p align="center">
    <a href="https://discord.gg/edCTVFVwnV">
        <img alt="Discord" src="https://img.shields.io/discord/1085050447410241556?style=flat&logo=discord&label=discord">
    </a>
    <a href="https://github.com/softwareantics/FinalEngine/issues?q=is%3Aopen+is%3Aissue+label%3A%22%F0%9F%8F%81+Good+First+Issue%22">
        <img alt="GitHub issues" src="https://img.shields.io/github/issues/softwareantics/FinalEngine/🏁%20Good%20First%20Issue?color=7057ff&label=Good%20First%20Issues">
    </a>
</p>

<p align="center">
    <a href="#key-features">Key Features</a> •
    <a href="#getting-started">Getting Started</a> •
    <a href="#download">Download</a> •
    <a href="#contributing">Contributing</a> 
</p>

<p align="center">
    <img src="https://user-images.githubusercontent.com/50978201/202500840-07f0a568-633b-4494-99af-4ca0e17afd4f.png" alt="Screenshot">
</p>

## Key Features

### Cross-Platform Compatibility

Built using C# 11 and .NET 7.0, the engine boasts excellent cross-platform compatibility. It's important to note that while the engine is actively in development, some features might not function as expected on certain platforms. If you encounter any issues while using _Final Engine_ on a specific platform, please don't hesitate to [report it](https://github.com/softwareantics/FinalEngine/issues/new/choose).

### Advanced Rendering API

_Final Engine_ offers a meticulously designed, feature-rich rendering abstraction layer built over OpenGL (with plans to support additional backends like Direct3D and Vulkan in the future). This API empowers users to engage directly with the graphics card while also providing systems and features for easily rendering meshes and sprites within scenes.

### Rendering Engine

_Final Engine_ has a powerful forward rendering engine that encapsulates a blend of simplicity and robust capabilities. Continuously evolving through active development, the current feature set includes:

- **Forward Rendering**: Embraces a classical rendering pipeline for efficient processing.
- **Mesh Rendering**: Empowers diverse rendering of intricate mesh structures.
- **Camera System**: Orchestrates scene views and perspectives seamlessly.
- **Material System**: Enables the utilization of Diffuse, Specular, and Normal Mapping for versatile surface aesthetics.
- **Lighting**: Embodies the esteemed Blinn-Phong Lighting Model for lifelike illumination.
- **Light Sources**: Encompasses Directional, Point, and Spot lights for scene ambiance.
- **Global Illumination**: Augments scene realism through ambient lighting.

More rendering features are constantly being added to _Final Engine_, check out the [features](https://github.com/softwareantics/FinalEngine/blob/master/Documentation/RenderingFeatures.md) list.

### Entity-Component-System (ECS) Architecture

Driven by the ECS architectural pattern, _Final Engine_ employs the entity-component-system model to power its core functionality. Learn more about ECS [here](https://en.wikipedia.org/wiki/Entity_component_system). This architectural choice serves as the foundation of the engine, enabling swift game design while maintaining a clear separation of concerns.

### Effortless Resource Management

_Final Engine_ incorporates a straightforward and user-friendly _Resource Manager_ to handle various resources utilized within the engine (such as audio, textures, and shaders). Expanding the engine with new resources is made simple through the `ResourceLoaderBase` abstraction and the `IResourceManager.RegisterLoader` method.

### [Desktop Editor (Work in Progress)](#editor)

We're actively developing an editor application for creating games using _Final Engine_. This editor will grant users the ability to craft entities, components, systems, manage projects, scenes, and resources. Our goal is to offer a preview of the editor by 2024.

![image](https://github.com/softwareantics/FinalEngine/assets/50978201/6bbacbce-7746-41b7-9e15-4ffb5699ec56)

## Getting Started

Below are the prerequisites and instructions for building the engine.

Feel free to reach out to us on [Discord](https://discord.gg/edCTVFVwnV) if you encounter any challenges while setting up the engine or working with its features. Please note that comprehensive user documentation, including tutorials, have not yet been provided. Your inquiries and feedback are greatly valued as they contribute to the ongoing development of our project.

### Prerequisites

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0).

### Building on Windows, Mac, and Linux

1. Download or clone the repository.
2. Open `FinalEngine.sln` in your preferred IDE.
3. Build the solution (or use `dotnet build`).

### Download

- Release builds will be accessible on GitHub and as NuGet packages.

## Contributing

Follow our contribution guidelines [here](https://github.com/softwareantics/FinalEngine/blob/master/.github/CONTRIBUTING.md) if you wish to contribute to the project.
