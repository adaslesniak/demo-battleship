using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    //this whole api should be done async, so clients can be remote, but... yagni
    internal interface IPlayerInterface
    {
        public enum Messages { DidMiss, DidHit, WasHit, NoDamage, Loss, Vicotry }

        //TODO FIXME: this should take in settings param which would contain ship types as well
        Map Initialize(string withName, GameSettings setup);
        Coordinates Shoot();
        void Communicate(Messages message);
    }
}
