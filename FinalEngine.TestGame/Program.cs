using System;
using System.Diagnostics;
using System.Drawing;
using FinalEngine.ECS;
using FinalEngine.ECS.Components.Core;
using FinalEngine.ECS.Components.Rendering;
using FinalEngine.ECS.Systems.Input;
using FinalEngine.ECS.Systems.Rendering;
using FinalEngine.Extensions.Resources.Invocation;
using FinalEngine.Extensions.Resources.Loaders;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.IO;
using FinalEngine.IO.Invocation;
using FinalEngine.Platform.Desktop.OpenTK;
using FinalEngine.Platform.Desktop.OpenTK.Invocation;
using FinalEngine.Rendering;
using FinalEngine.Rendering.OpenGL;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Resources;
using FinalEngine.Runtime;
using FinalEngine.Runtime.Invocation;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

var settings = new NativeWindowSettings()
{
    API = ContextAPI.OpenGL,
    APIVersion = new Version(4, 5),

    Flags = ContextFlags.ForwardCompatible,
    Profile = ContextProfile.Core,

    AutoLoadBindings = false,

    WindowBorder = WindowBorder.Fixed,
    WindowState = WindowState.Normal,

    Size = new OpenTK.Mathematics.Vector2i(1280, 720),

    StartVisible = true,
};

var nativeWindow = new NativeWindowInvoker(settings);

var window = new OpenTKWindow(nativeWindow)
{
    Title = "Final Engine",
};

var keyboardDevice = new OpenTKKeyboardDevice(nativeWindow);
var keyboard = new Keyboard(keyboardDevice);

var mouseDevice = new OpenTKMouseDevice(nativeWindow);
var mouse = new Mouse(mouseDevice);

var file = new FileInvoker();
var directory = new DirectoryInvoker();
var fileSystem = new FileSystem(file, directory);

var opengl = new OpenGLInvoker();
var bindings = new GLFWBindingsContext();

var renderContext = new OpenGLRenderContext(opengl, bindings, nativeWindow.Context);
var renderDevice = new OpenGLRenderDevice(opengl);

ResourceManager.Instance.RegisterLoader(new Texture2DResourceLoader(fileSystem, renderDevice.Factory, new ImageInvoker()));
ResourceManager.Instance.RegisterLoader(new ShaderResourceLoader(renderDevice.Factory, fileSystem));

var renderingEngine = new RenderingEngine(renderDevice, fileSystem);

var watch = new Stopwatch();
var watchInvoker = new StopwatchInvoker(watch);
var gameTime = new GameTime(watchInvoker, 120.0d);

MeshVertex[] vertices =
{
    new MeshVertex()
    {
        Position = new System.Numerics.Vector3(-1, -1, 0),
        Color = new System.Numerics.Vector4(1, 0, 0, 1),
        TextureCoordinate = new System.Numerics.Vector2(0, 0),
    },

    new MeshVertex()
    {
        Position = new System.Numerics.Vector3(1, -1, 0),
        Color = new System.Numerics.Vector4(0, 1, 0, 1),
        TextureCoordinate = new System.Numerics.Vector2(1, 0),
    },

    new MeshVertex()
    {
        Position = new System.Numerics.Vector3(0, 1, 0),
        Color = new System.Numerics.Vector4(0, 0, 1, 1),
        TextureCoordinate = new System.Numerics.Vector2(0.5f, 1),
    },
};

int[] indices =
{
    0, 1, 2,
};

var mesh = new Mesh(renderDevice.Factory, vertices, indices);
var material = new Material();

var world = new EntityWorld();

var camera = new Entity();

camera.AddComponent<TransformComponent>();

camera.AddComponent(new PerspectiveComponent()
{
    AspectRatio = 1280.0f / 720.0f,
    FarPlaneDistance = 1000.0f,
    NearPlaneDistance = 0.1f,
    FieldOfView = 70.0f,
});

camera.AddComponent(new CameraComponent()
{
    IsEnabled = true,
    IsLocked = false,
    Viewport = new Rectangle(0, 0, 1280, 720),
});

camera.AddComponent(new VelocityComponent()
{
    Speed = 0.1f,
});

world.AddEntity(camera);

var cameraSystem = new CameraUpdateEntitySystem(keyboard, mouse);

world.AddSystem(cameraSystem);
world.AddSystem(new SceneRenderEntitySystem(renderDevice, renderingEngine)
{
    Camera = camera,
});

var entity = new Entity();

entity.AddComponent<TransformComponent>();
entity.AddComponent(new ModelComponent()
{
    Material = material,
    Mesh = mesh,
});

world.AddEntity(entity);

bool isRunning = true;

while (isRunning)
{
    if (!gameTime.CanProcessNextFrame())
    {
        continue;
    }

    cameraSystem.Process();

    keyboard.Update();
    mouse.Update();

    renderingEngine.Render();

    renderContext.SwapBuffers();
    window.ProcessEvents();
}

ResourceManager.Instance.Dispose();

renderContext.Dispose();
mouse.Dispose();
keyboard.Dispose();
window.Dispose();
nativeWindow.Dispose();
