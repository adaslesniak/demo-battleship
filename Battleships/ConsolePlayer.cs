namespace Battleships;

internal class ConsolePlayer : IPlayerInterface {
    string name = "The player";
    GameSettings? setup;
    int hitsOnEnemy = 0;
    int playerShipsTonnage = 0;

    Map IPlayerInterface.Initialize(string withName, GameSettings rules) {
        setup = rules;
        name = withName;
        var map =  Map.Prepare(setup, setup.RandomDeployment());
        playerShipsTonnage = map.OccupiedFields();
        WriteOnScreen(($"Your ships were deployed. There are {playerShipsTonnage} tonns of them.", ConsoleColor.Yellow));
        WriteOnScreen(($"Good luck admiral {withName}", ConsoleColor.Yellow));
        return map;
    }

    Coordinates IPlayerInterface.Shoot() {
        if(setup is null) {
            throw new Exception("Player not initialized");
        }
        Console.WriteLine($"{name} provide coordinates for fire: ");
        var userInput = Console.ReadLine();
        if(false == Coordinates.TryParse(userInput, out var properlyParsed, setup.mapSize)) {
            var randomTarget = setup.RandomPlace();
            Console.WriteLine($"bad aiming, fire went randomly at: {randomTarget}");
            return randomTarget;
        } else {
            return properlyParsed;
        }
    }

    void IPlayerInterface.Communicate(IPlayerInterface.Messages message) {
        ActionOnMessage(message)?.Invoke();
        WriteOnScreen(UserMessage(message));        
    }

    void WriteOnScreen((string, ConsoleColor) info) { 
        Console.ForegroundColor = info.Item2;
        Console.WriteLine(info.Item1);
        Console.ForegroundColor = ConsoleColor.White;
    }

    Action ActionOnMessage(IPlayerInterface.Messages message) => message switch {
        IPlayerInterface.Messages.DidHit => () => hitsOnEnemy++,
        IPlayerInterface.Messages.WasHit => () => playerShipsTonnage--,
        _ => () => { }
    };

    (string, ConsoleColor) UserMessage(IPlayerInterface.Messages message) => message switch {
        IPlayerInterface.Messages.DidMiss => ($"{name} missed...", ConsoleColor.DarkGray),
        IPlayerInterface.Messages.DidHit => ($"{name} - fire on TARGET ({hitsOnEnemy} hits in total)", ConsoleColor.Green),
        IPlayerInterface.Messages.NoDamage => ($"{name} no damage recieved", ConsoleColor.DarkGray),
        IPlayerInterface.Messages.WasHit => ($"{name} - your ship was hit. {playerShipsTonnage} tons of your ships remains.", ConsoleColor.Red),
        IPlayerInterface.Messages.Vicotry => ($"{name} You won, good job!", ConsoleColor.Green),
        IPlayerInterface.Messages.Loss => ($"{name} You lost, better luck next time", ConsoleColor.Red),
        _ => ("maybe it doesn't matter", ConsoleColor.Magenta) };
}
