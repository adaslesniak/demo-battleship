using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships;
internal class GameLoop
{
    readonly Player playerOne;
    readonly Player playerX;

    internal GameLoop(Player attacker, Player defender) {
        playerOne = attacker;
        playerX = defender;
    }

    internal void Run() {
        Player? winner = null;
        while(winner is null) {
            Round(playerOne, playerX, out var isEnd);
            if(isEnd) {
                winner = playerOne;
                break;
            }
            Round(playerX, playerOne, out isEnd);
            if(isEnd) {
                winner = playerX;
                break;
            }
        }
        DeclareEnd(winner);
    }

    void DeclareEnd(Player winner) {
        var looser =
            winner == playerOne
            ? playerX
            : playerOne;
        winner.ui.Communicate(IPlayerInterface.Messages.Vicotry);
        looser.ui.Communicate(IPlayerInterface.Messages.Loss);
    }

    void Round(Player attacker, Player defender, out bool isGameOver) {
        var shot = attacker.ui.Shoot();
        var didHit = defender.state.HitField(shot);
        attacker.ui.Communicate(
            didHit
            ? IPlayerInterface.Messages.DidHit
            : IPlayerInterface.Messages.DidMiss);
        defender.ui.Communicate(
            didHit
            ? IPlayerInterface.Messages.WasHit
            : IPlayerInterface.Messages.NoDamage);
        isGameOver = defender.state.IsAlive() is false;
    }
}
