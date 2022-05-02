using System.Collections.Generic;
using BossChess.Components.AI;
using BossChess.Interfaces;

namespace BossChess.Components;

public class GameManager
{
    private IBoard currentBoard;

    private bool isWhitesTurn = true;
    private AbstractPlayer whitePlayer;
    private AbstractPlayer blackPlayer;

    public GameManager(AbstractPlayer whitePlayer, AbstractPlayer blackPlayer)
    {
        this.whitePlayer = whitePlayer;
        this.whitePlayer.SetSubmitMoveAction(this.SubmitMoveAction);
        
        this.blackPlayer = blackPlayer;
        this.blackPlayer.SetSubmitMoveAction(this.SubmitMoveAction);
    }

    public void SubmitMoveAction(IMove move)
    {
        isWhitesTurn=!isWhitesTurn;
        currentBoard = BoardGenerator.GenerateNewBoardWithMove(currentBoard, move);

        AuthorizeNextPlayer();
    }

    public void AuthorizeNextPlayer()
    {
        if (isWhitesTurn)
        {
            whitePlayer.AuthorizeToMakeMove();
        }
        else
        {
            blackPlayer.AuthorizeToMakeMove();
        }
    }
}