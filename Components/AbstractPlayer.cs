using System;
using BossChess.Interfaces;

namespace BossChess.Components;

public abstract class AbstractPlayer
{
    protected Action<IMove> SubmitMove;
    protected IBoard currentBoard;

    public void SetSubmitMoveAction(Action<IMove> move)
    {
        SubmitMove = move;
    }

    public void SetCurrentBoardReference(ref IBoard currentBoard)
    {
        this.currentBoard = currentBoard;
    }

    public abstract void AuthorizeToMakeMove();
}