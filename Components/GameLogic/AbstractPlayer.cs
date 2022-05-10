using System;
using BossChess.Interfaces;

namespace BossChess.Components;

public abstract class AbstractPlayer
{
    protected Action<IMove> SubmitMove;
    protected GameManager gm;

    public void SetSubmitMoveAction(Action<IMove> move)
    {
        SubmitMove = move;
    }

    public void SetGameManager(GameManager gm)
    {
        this.gm = gm;
    }

    public abstract void AuthorizeToMakeMove();
}