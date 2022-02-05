namespace TestGame
{
    internal static class Program
    {
        private static void Main()
        {
            using (var game = new Game())
            {
                game.Launch(120.0d);
            }
        }
    }
}