using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships;

//this is crap not any proper ui, so not even worthy tests,
//but interface was not the thing of this project
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
        Console.WriteLine($"Your ships were deployed. There are {playerShipsTonnage} tonns of them.");
        Console.WriteLine($"Good luck admiral {withName}");
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
        Console.WriteLine(TextMessage(message));
    }
        

    Action ActionOnMessage(IPlayerInterface.Messages message) => message switch {
        IPlayerInterface.Messages.DidHit => () => hitsOnEnemy++,
        IPlayerInterface.Messages.WasHit => () => playerShipsTonnage--,
        _ => () => { }
    };

    string TextMessage(IPlayerInterface.Messages message) => message switch {
        IPlayerInterface.Messages.DidMiss => $"{name} missed",
        IPlayerInterface.Messages.DidHit => $"{name} - fire on TARGET ({hitsOnEnemy} hits in total)",
        IPlayerInterface.Messages.NoDamage => $"{name} no damage recieved",
        IPlayerInterface.Messages.WasHit => $"{name} - your ship was hit. {playerShipsTonnage} tons of ships remains)",
        IPlayerInterface.Messages.Vicotry => $"{name} You won, good job",
        IPlayerInterface.Messages.Loss => $"{name} You lost, better luck next time",
        _ => "maybe it doesn't matter" };
}
