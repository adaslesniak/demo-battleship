using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    internal class GameSettings
    {
        internal readonly byte mapSize;
        internal readonly Ship.Factory[] initialShips;

        internal GameSettings(byte wihtMapSize, params Ship.Factory[] withShips) {
            this.mapSize = wihtMapSize;
            this.initialShips = withShips;
        }
    }
}
