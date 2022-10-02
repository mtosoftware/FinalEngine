namespace FinalEngine.Examples.HelloWindow
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Launch(frameCap: 120.0d);
            }
        }
    }
}