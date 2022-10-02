![GitHub-Mark-Light](https://user-images.githubusercontent.com/50978201/193459338-32d71599-19d6-4eb6-b5b3-c34348d623b9.svg#gh-dark-mode-only)![GitHub-Mark-Dark](https://user-images.githubusercontent.com/50978201/193459322-b078ed0d-cf0d-4791-ad10-ee2f3131cd20.svg#gh-light-mode-only)

_Final Engine_ is a *WIP* cross-platform _Game Engine_ developed in C# 9.0, using .NET 5.0. _FE_ originally started as a hobby project but has quickly developed into something that I wish to maintain and actively develop as time goes on. The primary goal and focus of _Final Engine_ is to provide a feature-rich 2D/3D _Game Engine_ that keeps the idea of simplicity and new users in mind. The engine currently supports Windows, Macintosh and Linux operating systems with support for mobile and console backends to be provided after the initial first release. Our mission statement:

> Create an engine that will make game development fun, simple and easy whilst giving the end user complete freedom.

## Features

Below you'll see a simple breakdown of the current features provided. Please note that _Final Engine_ is under active development and some features are continuously changing. Up until the first release we will make no efforts to maintain or keep leagcy/deprecated code.

#### Cross Platform 

_Final Engine_ can currently runs games on _Windows_, _Macintosh_ and _Linux_ operating systems seamlessly. Support for _Android_ and _iOS_ development will commence after the initial release.

#### Modular

Most logic within _Final Engine_ can be replaced because of the thorough use of the _Interface Segregation Orinciple_. We want to have the ability to provide end users with as must versatility as possible whilst allowing them to get setup quickly and easily. You don't like how we handle rendering? That's fine, replace it. You want to add support for a new input device? Go ahead! We're missing support for a new platform? Add it in! You get the idea.

Another cool feature of _Final Engine_ is the solution is **not** monolithic. If you want to use a feature from our engine within another (such as Unity or Unreal) you can go right ahead! Whilst we don't maintain support for integrating with other engines we still like to keep a top-down kind of structure and allow the user to only reference libraries that are required. If you only want to use our _Rendering API_, you can do so, etc.

#### Feature-rich Rendering API

_Final Engine_ currently uses _OpenGL 4.5_ under the hood but active development is underway to provide support for other APIs such as _DirectX_, _Vulkan_ and _Metal_. You should ultimately be able to modify which API you want to use prior to runtime via some sort of configuration option. The current abstraction layer loosely mimics the _DirectX_ API and provides support for most requirements such as:

- Buffers
  - Vertex Buffers
  - Index Buffers
  - Input Layouts
- Shaders
- Shader Programs
- 2D Textures

The API will change over each release cycle iteration but only to accomodate new requirements (such as future support for 3D, we will need _Render Targets_).

#### Input

_Final Engine_ currently only supports standard _Keyboard_ and _Mouse_ input; however, work is underway to allow support for _Game Controllers_ and _Joysticks_. You can expect a feature-rich _Input Library_ for the first release.

#### 2D Sprite Batching

_Final Engine_ provides a standardized _Sprite Batching_ system which utilizies a few classes and rendering techniques to provide seamless and fast sprite batching for most requirements.

#### Entity Component System based Architecture

_ECS_ is a bit of a buzz word thrown around in the game development industry. That being said, there's a time and a place for it. The current implementation of our _ECS_ framework is pretty bare-bones but it gets the job done.

### Build Instructions

Below is a list of prerequisites and build instructions.

#### Prerequisites

- [.NET 5.0 SDK](https://dotnet.microsoft.com/download/dotnet/5.0).

#### Windows, Mac and Linux

1. Download or clone the repository.
2. Open `FinalEngine.sln` your favourite IDE.
3. Build the solution (or `dotnet build`).
4. Run the *TestGame* project (or `dotnet run`). 

### Download

Release builds will be available as NuGet packages.

### Contributing

If you do not follow the steps below, it is unlikely your PR will be merged.

1. Install [Git](https://git-scm.com/downloads) and the [.NET 5.0 SDK](https://dotnet.microsoft.com/download/dotnet/5.0).
2. [Fork](https://github.com/mtosoftware/FinalEngine/fork) the repository.
3. Create a new branch of develop on your fork, make sure the branch name is descriptive.
4. Add an empty commit to start your work off: `git commit --allow-empty -m "[WIP] Thing you're doing"`.
5. Open a Pull request with `[WIP]` in the title; following the template. Do this **before** you start working.
6. Make your commits in *small*, *incremental* steps.
7. Tag a maintainer when you're done and ask for your PR to be merged (don't forget about unit tests!).

### Credits

- [Learn OpenGL](https://learnopengl.com/)
- [DirectX Tutorials](http://www.directxtutorial.com/)
- [TheBennyBox](https://www.youtube.com/user/thebennybox)
- [ThinMatrix](https://www.youtube.com/user/ThinMatrix)

[![Hits](https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https%3A%2F%2Fgithub.com%2Fsoftwareantics%2FFinalEngine&count_bg=%2379C83D&title_bg=%23555555&icon=&icon_color=%23E7E7E7&title=hits&edge_flat=false)](https://hits.seeyoufarm.com)
