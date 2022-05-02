using System;
using System.Collections.Generic;
using BossChess.Interfaces;
using Microsoft.Xna.Framework;

namespace BossChess.Components;

public class HumanPlayer : AbstractPlayer
{
    private bool IsTurn;

    private List<IMove> currentMoves;
    
    public void RegisterClick(Point p)
    {
        if (IsTurn)
        {
            PrimitivePiece pieceAtClick = currentBoard.GetPieceAt(p);
            if (pieceAtClick.Type!=PieceType.None && pieceAtClick.IsWhite==currentBoard.isWhitesTurn)
            {
                currentMoves = BoardGenerator.GenerateValidMovesAt(currentBoard, p);
            }
            else
            {
                foreach (IMove m in currentMoves)
                {
                    Point moveLoc = m.ActualMoves[0].to;
                    if (moveLoc==p)
                    {
                        SubmitMove(m);
                        IsTurn = false;
                    }
                }
            }
        }
    }

    public override void AuthorizeToMakeMove()
    {
        IsTurn = true;
    }
}