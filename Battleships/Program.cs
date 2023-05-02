namespace Battleships;
class Program
{
    const byte WorldSize = 10;


    static void Main(string[] args)
    {
        new GameLoop(
            new(new ConsolePlayer(), "playerOne", WorldSize),
            new(new ConsolePlayer(), "PlayerX", WorldSize))
            .Run();
        Console.WriteLine("that's all");
    }

    
}
