namespace Battleships;

//this whole api should be done async, so clients can be remote, but... yagni
internal interface IPlayerInterface
{
    public enum Messages { DidMiss, DidHit, WasHit, NoDamage, Loss, Vicotry }

    Map Initialize(string withName, GameSettings setup);
    Coordinates Shoot();
    void Communicate(Messages message);
}
