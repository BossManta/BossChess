using System.Collections.Generic;

namespace BossChess.Interfaces;

public interface IGameManager
{
    IMove GetBestMove();

    List<IMove> GetValidMoves();

    void MakeMove(int moveIndex);
}