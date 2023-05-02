using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    internal class RandomAlgorithmPlayer : IPlayerInterface
    {
        void IPlayerInterface.Communicate(IPlayerInterface.Messages message) { }

        PlayerWorld IPlayerInterface.Initialize(string withName, byte withWorldSize) {
            throw new NotImplementedException();
        }

        Coordinates IPlayerInterface.Shoot() {
            throw new NotImplementedException();
        }
    }
}
