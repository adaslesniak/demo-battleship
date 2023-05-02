using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships;

internal class ConsolePlayer : IPlayerInterface {
    string name = "The player";
    int worldSize = 10;

    PlayerWorld IPlayerInterface.Initialize(string withName, byte withWorldSize) {
        worldSize = withWorldSize;
        name = withName;
        return PlayerWorld.Prepare(withWorldSize, PlayerWorld.AutoDeployment(withWorldSize, Ship.Battleship, Ship.Destroyer, Ship.Destroyer));
    }

    Coordinates IPlayerInterface.Shoot() {
        Console.WriteLine($"{name} provide coordinates for fire: ");
        var userInput = Console.ReadLine();
        if(false == IsInputValid(userInput, out var column, out var row)) {
            var randomTarget = PlayerWorld.RandomPlace(worldSize);
            Console.WriteLine($"bad aiming, fire went randomly at: {randomTarget}");
            return randomTarget;
        } else {
            return new Coordinates(column, row);
        }
    }

    bool IsInputValid(string? userInput, out byte column, out byte row) {
        column = row = 0;
        return userInput?.Length == 2
            && Coordinates.TryParseColumn(userInput[0], out column)
            && column < worldSize
            && byte.TryParse(userInput.Substring(1), out row)
            && row < worldSize;
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
