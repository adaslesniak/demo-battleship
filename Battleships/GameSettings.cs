using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    public class GameSettings
    {
        public readonly byte mapSize;
        public readonly Ship.Factory[] initialShips;
        public readonly Random luck = new Random();

        public GameSettings(byte wihtMapSize, params Ship.Factory[] withShips) :
            this(wihtMapSize, DateTime.UtcNow.Microsecond, withShips) { }
            

        public GameSettings(byte wihtMapSize, int withSeedForRandom, params Ship.Factory[] withShips) {
            this.mapSize = wihtMapSize;
            this.initialShips = withShips;
            this.luck = new(withSeedForRandom);
        }
    }
}
