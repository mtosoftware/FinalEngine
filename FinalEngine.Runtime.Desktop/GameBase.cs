namespace FinalEngine.Runtime.Desktop;

public abstract class GameBase : GameContainerBase
{
    public GameBase()
        : base(new DesktopRuntimeFactory())
    {
    }
}
