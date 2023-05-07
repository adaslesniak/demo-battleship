namespace Battleships;
class Program
{
    static readonly GameSettings options = new(
        wihtMapSize: 10,
        Ship.Battleship, Ship.Destroyer, Ship.Destroyer);

    static void Main(string[] args)
    {
        new GameLoop(
            new(new ConsolePlayer(), "playerOne", options),
            new(new RandomAlgorithmPlayer(), "PlayerX", options))
            .Run();
        Console.WriteLine("that's all");
    }
}
