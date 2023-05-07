using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships;

//not necessary, but it's easier to play with this
//no tests as it's not required part
internal class RandomAlgorithmPlayer : IPlayerInterface
{
    HashSet<Coordinates> firedShots = new();
    byte worldSize = 0;
    
    void IPlayerInterface.Communicate(IPlayerInterface.Messages message) { }

    Map IPlayerInterface.Initialize(string withName, GameSettings rules) {
        worldSize = rules.mapSize;
        return Map.Prepare(worldSize, Map.AutoDeployment(rules));
    }

    Coordinates IPlayerInterface.Shoot() {
        Coordinates randomPlace;
        do {
            randomPlace = Map.RandomPlace(worldSize);
        } while(firedShots.Contains(randomPlace));
        firedShots.Add(randomPlace);
        return randomPlace;
    }


}
