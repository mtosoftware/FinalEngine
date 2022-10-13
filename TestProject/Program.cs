using System.Drawing;
using FinalEngine.Runtime.Desktop;
using FinalEngine.Runtime.Settings;
using TestProject;

var runtime = new DesktopRuntimeFactory();
var settings = new GameSettings()
{
    FrameCap = 120.0d,
    WindowSettings = new WindowSettings()
    {
        Size = new Size(1280, 720),
        Title = "My Game",
    }
};

using (var game = new Game(settings, runtime))
{
    game.Run();
}
