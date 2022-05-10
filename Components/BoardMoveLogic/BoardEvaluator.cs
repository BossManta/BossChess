using System;
using BossChess.Interfaces;

namespace BossChess.Components;

public class BoardEvaluator
{
    public float GetValue(IBoard board)
    {
        PieceLogicProvider plp = PieceLogicProvider.GetGlobalInstance();
        var primGrid = board.PrimitivePieceGrid;
        float totalVal = 0;
        for (int x=0;x<primGrid.GetLength(0);x++)
        {
            for (int y=0;y<primGrid.GetLength(1);y++)
            {
                PrimitivePiece p = primGrid[x,y];
                if (p.Type == PieceType.None)
                {
                    continue;
                }


                int pieceVal = Piece.GetValue(p.Type);
                float v = pieceVal;
                v+= 1/(Math.Abs(y-3.5F)+0.5F);
                v+= 1/(Math.Abs(x-3.5F)+0.5F);

                if (p.IsWhite)
                {
                    totalVal+=v;
                }
                else
                {
                    totalVal-=v;
                }
            }
        }
        return totalVal;
    }
}