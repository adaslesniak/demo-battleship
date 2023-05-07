namespace Battleships;

//not required, but it's easier to play with this
//no tests as it's not required part
internal class RandomAlgorithmPlayer : IPlayerInterface
{
    HashSet<Coordinates> firedShots = new();
    GameSettings? setup;
    
    void IPlayerInterface.Communicate(IPlayerInterface.Messages message) { }

    Map IPlayerInterface.Initialize(string withName, GameSettings withRules) {
        setup = withRules;
        return Map.Prepare(setup, setup.RandomDeployment());
    }

    Coordinates IPlayerInterface.Shoot() {
        if(setup is null) {
            throw new Exception("player not initialized");
        }
        Coordinates randomPlace;
        do {
            randomPlace = setup.RandomPlace();
        } while(firedShots.Contains(randomPlace));
        firedShots.Add(randomPlace);
        return randomPlace;
    }


}
