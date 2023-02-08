namespace TestGame;

internal static class Program
{
    internal static void Main()
    {
        using (var game = new Game())
        {
            game.Run(120.0d);
        }
    }
}
