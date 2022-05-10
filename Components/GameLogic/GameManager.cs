using System.Collections.Generic;
using BossChess.Components.AI;
using BossChess.Interfaces;

namespace BossChess.Components;

public class GameManager
{
    public IBoard currentBoard { get; private set; }

    private bool waitingForMove = false;
    private AbstractPlayer whitePlayer;
    private AbstractPlayer blackPlayer;

    public GameManager(AbstractPlayer whitePlayer, AbstractPlayer blackPlayer)
    {
        this.whitePlayer = whitePlayer;
        this.whitePlayer.SetSubmitMoveAction(this.SubmitMoveAction);
        this.whitePlayer.SetGameManager(this);
        
        this.blackPlayer = blackPlayer;
        this.blackPlayer.SetSubmitMoveAction(this.SubmitMoveAction);
        this.blackPlayer.SetGameManager(this);

        //Refactor BoardGenerator to generate default starting board
        currentBoard = new Board();
    }

    public void SubmitMoveAction(IMove move)
    {
        currentBoard = BoardGenerator.GenerateNewBoardWithMove(currentBoard, move);
        waitingForMove = false;
    }

    public void AuthorizeNextPlayer()
    {
        if (currentBoard.isWhitesTurn)
        {
            whitePlayer.AuthorizeToMakeMove();
        }
        else
        {
            blackPlayer.AuthorizeToMakeMove();
        }
        waitingForMove = true;
    }

    public void Update()
    {
        if (!waitingForMove && !currentBoard.IsCheckMated(currentBoard.isWhitesTurn))
        {
            AuthorizeNextPlayer();
        }
    }
}