namespace Battleships;

class Player
{
    public readonly IPlayerInterface ui;
    public readonly Map state;

    public Player(IPlayerInterface withUI, string withName, GameSettings withRules) {
        ui = withUI;
        state = withUI.Initialize(withName, withRules);
    }
}
