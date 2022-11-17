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
using FinalEngine.Rendering.Data;
using FinalEngine.Rendering.OpenGL;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Rendering.Renderers;
using FinalEngine.Resources;
using FinalEngine.Runtime;
using FinalEngine.Runtime.Invocation;
using FinalEngine.TestGame;
using ImGuiNET;
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

    Size = new OpenTK.Mathematics.Vector2i(1600, 900),

    StartVisible = true,

    //// TODO: Issue #159
    NumberOfSamples = 8,
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

var geometryRenderer = new GeometryRenderer(renderDevice);
var renderingEngine = new RenderingEngine(renderDevice, fileSystem, geometryRenderer);

ResourceManager.Instance.RegisterLoader(new ShaderResourceLoader(renderDevice.Factory, fileSystem));
ResourceManager.Instance.RegisterLoader(new ShaderProgramResourceLoader(renderDevice.Factory, fileSystem));
ResourceManager.Instance.RegisterLoader(new Texture2DResourceLoader(fileSystem, renderDevice.Factory, new ImageInvoker(), renderingEngine));
ResourceManager.Instance.RegisterLoader(new ModelResourceLoader(renderDevice, fileSystem));

renderingEngine.Initialize();

var model = ResourceManager.Instance.LoadResource<Model>("Resources\\Models\\Sponza\\sponza.obj");

var watch = new Stopwatch();
var watchInvoker = new StopwatchInvoker(watch);
var gameTime = new GameTime(watchInvoker, 120.0d);

var world = new EntityWorld();

var camera = new Entity();

camera.AddComponent<TransformComponent>();

camera.AddComponent(new PerspectiveComponent()
{
    AspectRatio = 1600.0f / 900.0f,
    FarPlaneDistance = 10000.0f,
    NearPlaneDistance = 0.1f,
    FieldOfView = 70.0f,
});

camera.AddComponent(new CameraComponent()
{
    IsEnabled = true,
    IsLocked = false,
    Viewport = new Rectangle(0, 0, 1600, 900),
});

camera.AddComponent(new VelocityComponent()
{
    Speed = 0.4f,
});

world.AddEntity(camera);

var cameraSystem = new CameraUpdateEntitySystem(keyboard, mouse);
world.AddSystem(cameraSystem);

bool isRunning = true;

var controller = new ImGuiController(1600, 900);

var ambientColor = new Vector3(126 / 255.0f, 18 / 255.0f, 18 / 255.0f);
var diffuseColor = new Vector3(191 / 255.0f, 149 / 255.0f, 89 / 255.0f);
var specularColor = new Vector3(188 / 255.0f, 128 / 255.0f, 52 / 255.0f);
var direction = new Vector3(-1, -1, -1);

var batcher = new SpriteBatcher(renderDevice.InputAssembler);
var binder = new TextureBinder(renderDevice.Pipeline);
var drawer = new SpriteDrawer(renderDevice, batcher, binder, 1600, 900);

var _ = new Vector3(5);
var scale = new Vector3(0.3f);

while (isRunning)
{
    if (!gameTime.CanProcessNextFrame())
    {
        continue;
    }

    window.Title = $"{GameTime.FrameRate}";

    world.ProcessAll(GameLoopType.Update);

    for (int i = 0; i < model.ModelDatas.Count; i++)
    {
        geometryRenderer.AddGeometry(new GeometryData(model.ModelDatas[i].Material, model.ModelDatas[i].Mesh, Matrix4x4.CreateScale(scale)));
    }

    var cameraData = new CameraData()
    {
        Projection = camera.GetComponent<PerspectiveComponent>().CreateProjectionMatrix(),
        View = camera.GetComponent<TransformComponent>().CreateViewMatrix(Vector3.UnitY),
        ViewPostiion = camera.GetComponent<TransformComponent>().Position,
    };

    controller.Update(keyboard, mouse, GameTime.Delta);

    keyboard.Update();
    mouse.Update();
    renderingEngine.diffuseColor = diffuseColor;
    renderingEngine.specularColor = specularColor;
    renderingEngine.ambientColor = ambientColor;
    renderingEngine.direction = direction;

    renderingEngine.Render(cameraData);

    drawer.Begin();
    drawer.Draw(
        new Material().DiffuseTexture,
        Color.Red,
        Vector2.Zero,
        Vector2.Zero,
        0,
        new Vector2(256, 256));
    drawer.End();

    ImGui.Begin("Tools");

    ImGui.ColorEdit3("Ambient Color", ref ambientColor);
    ImGui.ColorEdit3("Diffuse Color", ref diffuseColor);
    ImGui.ColorEdit3("Specular Color", ref specularColor);
    ImGui.DragFloat3("Direction", ref direction, 0.1f, -1, 1);
    ImGui.InputFloat3("Scale", ref scale);

    ImGui.End();

    controller.Render();

    renderContext.SwapBuffers();
    window.ProcessEvents();
}

renderingEngine.Dispose();

ResourceManager.Instance.Dispose();

renderContext.Dispose();
mouse.Dispose();
keyboard.Dispose();
window.Dispose();
nativeWindow.Dispose();
