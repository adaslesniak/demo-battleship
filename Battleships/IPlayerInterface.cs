using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    internal interface IPlayerInterface
    {
        public enum Messages { DidMiss, DidHit, WasHit, NoDamage, Loss, Vicotry }

        PlayerWorld Initialize(string withName, byte withWorldSize);
        Coordinates Shoot();
        void Communicate(Messages message);
    }
}
