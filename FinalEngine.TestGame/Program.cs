using System;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using FinalEngine.ECS;
using FinalEngine.ECS.Components.Core;
using FinalEngine.ECS.Components.Rendering;
using FinalEngine.ECS.Systems.Input;
using FinalEngine.Extensions.Resources.Invocation;
using FinalEngine.Extensions.Resources.Loaders;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.IO;
using FinalEngine.IO.Invocation;
using FinalEngine.Platform.Desktop.OpenTK;
using FinalEngine.Platform.Desktop.OpenTK.Invocation;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Lighting;
using FinalEngine.Rendering.OpenGL;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Resources;
using FinalEngine.Runtime;
using FinalEngine.Runtime.Invocation;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

var renderQualitySettings = new RenderQualitySettings()
{
    AntiAliasing = AntiAliasing.FourTimesMultiSampling,
    MultiSamplingEnabled = true,
};

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

    NumberOfSamples = renderQualitySettings.AntiAliasingSamples,
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
ResourceManager.Instance.RegisterLoader(new ShaderProgramResourceLoader(renderDevice.Factory, fileSystem));
ResourceManager.Instance.RegisterLoader(new ModelResourceLoader(renderDevice, fileSystem));

var renderingEngine = new RenderingEngine(renderDevice, fileSystem, renderQualitySettings);

var model = ResourceManager.Instance.LoadResource<Model>("Resources\\Models\\Sponza\\sponza.obj");

var watch = new Stopwatch();
var watchInvoker = new StopwatchInvoker(watch);
var gameTime = new GameTime(watchInvoker, 120.0d);

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
    Speed = 0.4f,
});

world.AddEntity(camera);

var cameraSystem = new CameraUpdateEntitySystem(keyboard, mouse);
world.AddSystem(cameraSystem);

bool isRunning = true;

while (isRunning)
{
    if (!gameTime.CanProcessNextFrame())
    {
        continue;
    }

    window.Title = $"{GameTime.FrameRate}";

    world.ProcessAll(GameLoopType.Update);

    keyboard.Update();
    mouse.Update();

    renderingEngine.Enqueue(new DirectionalLight()
    {
        AmbientColor = new Vector3(0.2f),
        DiffuseColor = new Vector3(0.3f),
        SpecularColor = new Vector3(0.4f),
        Direction = new Vector3(-1, -1, -1),
    });

    for (int i = 0; i < model.ModelDatas.Count; i++)
    {
        renderingEngine.Enqueue(new GeometryData(model.ModelDatas[i].Material, model.ModelDatas[i].Mesh, Matrix4x4.CreateScale(0.4f)));
    }

    var cameraData = new CameraData()
    {
        Projection = camera.GetComponent<PerspectiveComponent>().CreateProjectionMatrix(),
        View = camera.GetComponent<TransformComponent>().CreateViewMatrix(Vector3.UnitY),
        ViewPostiion = camera.GetComponent<TransformComponent>().Position,
    };

    renderingEngine.Render(cameraData);

    renderContext.SwapBuffers();
    window.ProcessEvents();
}

ResourceManager.Instance.Dispose();

renderContext.Dispose();
mouse.Dispose();
keyboard.Dispose();
window.Dispose();
nativeWindow.Dispose();
