namespace FinalEngine.Examples.HelloWindow
{
    using FinalEngine.Input;
    using FinalEngine.Launching;

    public class Game : GameContainer
    {
        protected override void Update()
        {
            if (this.Keyboard.IsKeyReleased(Key.Escape))
            {
                Exit();
            }

            base.Update();
        }
    }
}