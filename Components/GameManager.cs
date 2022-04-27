using System.Collections.Generic;
using BossChess.Components.AI;
using BossChess.Interfaces;

namespace BossChess.Components;

public class GameManager : IGameManager
{
    private IBoard currentBoard;
    private int depth;
    private MinMaxAI AI;

    public GameManager(IBoard initialBoardLayout, int aiDepth)
    {
        currentBoard = initialBoardLayout;
        depth = aiDepth;

        AI.Init(initialBoardLayout, aiDepth);
    }

    public IMove GetBestMove()
    {
        throw new System.NotImplementedException();
    }

    public List<IMove> GetValidMoves()
    {
        throw new System.NotImplementedException();
    }

    public void MakeMove(int moveIndex)
    {
        throw new System.NotImplementedException();
    }
}