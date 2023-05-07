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
    byte worldSize = 10;

    Map IPlayerInterface.Initialize(string withName, GameSettings rules) {
        worldSize = rules.mapSize;
        name = withName;
        return Map.Prepare(worldSize, Map.AutoDeployment(rules));
    }

    Coordinates IPlayerInterface.Shoot() {
        Console.WriteLine($"{name} provide coordinates for fire: ");
        var userInput = Console.ReadLine();
        if(false == Coordinates.TryParse(userInput, out var properlyParsed, worldSize)) {
            var randomTarget = Map.RandomPlace(worldSize);
            Console.WriteLine($"bad aiming, fire went randomly at: {randomTarget}");
            return randomTarget;
        } else {
            return properlyParsed;
        }
    }

    void IPlayerInterface.Communicate(Battleships.IPlayerInterface.Messages message) =>
        Console.WriteLine(TextMessage(message));

    string TextMessage(IPlayerInterface.Messages message) => message switch {
        IPlayerInterface.Messages.DidMiss => $"{name} missed",
        IPlayerInterface.Messages.DidHit => $"{name} - fire on target",
        IPlayerInterface.Messages.NoDamage => $"{name} no damage recieved",
        IPlayerInterface.Messages.WasHit => $"{name} - your ship was hit",
        IPlayerInterface.Messages.Vicotry => $"{name} You won, good job",
        IPlayerInterface.Messages.Loss => $"{name} You lost, better luck next time",
        _ => "maybe it doesn't matter" };
}
