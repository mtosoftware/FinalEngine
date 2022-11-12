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
using FinalEngine.Rendering.Textures;
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
ResourceManager.Instance.RegisterLoader(new ShaderProgramResourceLoader(renderDevice.Factory, fileSystem));

var renderingEngine = new RenderingEngine(renderDevice, fileSystem);

var watch = new Stopwatch();
var watchInvoker = new StopwatchInvoker(watch);
var gameTime = new GameTime(watchInvoker, 120.0d);

float fieldDepth = 10.0f;
float fieldWidth = 10.0f;

MeshVertex[] vertices =
{
    new MeshVertex()
    {
        Position = new Vector3(-fieldWidth, 0.0f, -fieldDepth),
        Color = Vector4.One,
        TextureCoordinate = new Vector2(0, 0),
    },

    new MeshVertex()
    {
        Position = new Vector3(-fieldWidth, 0.0f, fieldDepth * 3),
        Color = Vector4.One,
        TextureCoordinate = new Vector2(0, 1),
    },

    new MeshVertex()
    {
        Position = new Vector3(fieldWidth * 3, 0.0f, -fieldDepth),
        Color = Vector4.One,
        TextureCoordinate = new Vector2(1, 0),
    },

    new MeshVertex()
    {
        Position = new Vector3(fieldWidth * 3, 0.0f, fieldDepth * 3),
        Color = Vector4.One,
        TextureCoordinate = new Vector2(1, 1),
    },
};

int[] indices =
{
    0, 1, 2, 2, 1, 3,
};

var mesh = new Mesh(renderDevice.Factory, vertices, indices);
var material = new Material()
{
    DiffuseTexture = ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\container.png"),
    SpecularTexture = ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\container_specular.png"),
    Shininess = 64.0f,
};

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

    window.Title = $"{GameTime.FrameRate}";

    cameraSystem.Process();

    keyboard.Update();
    mouse.Update();

    renderingEngine.Enqueue(new GeometryData(material, mesh, Matrix4x4.CreateTranslation(Vector3.Zero)));
    renderingEngine.Enqueue(new DirectionalLight()
    {
        AmbientColor = new Vector3(0.1f),
        DiffuseColor = new Vector3(0.3f),
        SpecularColor = new Vector3(1f, 1f, 1f),
        Direction = new Vector3(-1, -1, -1),
    });

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
