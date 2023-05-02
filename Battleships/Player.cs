using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships;

class Player
{
    public readonly IPlayerInterface ui;
    public readonly PlayerWorld state;

    public Player(IPlayerInterface withUI, string withName, byte worldSize) {
        ui = withUI;
        state = withUI.Initialize(withName, worldSize);
    }
}
