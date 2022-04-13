using BossChess.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BossChess.Components;

public class Pawn: Piece
{
    public override List<IMove> GetRawMoves(IBoard currentBoard, Point pos)
    {
        List<IMove> moves = new List<IMove>();

        PrimitivePiece currentPiece = currentBoard.GetPieceAt(pos);

        int direction = currentPiece.IsWhite?-1:1;

        IfEmptyAdd(moves, currentBoard, pos, 0, direction);
        if (!currentPiece.HasMoved)
        {
            IfEmptyAdd(moves, currentBoard, pos, 0, direction*2);
        }

        IfOnEnemyAt(moves, currentBoard, pos, -1, direction);
        IfOnEnemyAt(moves, currentBoard, pos, 1, direction);

        return moves;
    }

    public override List<IMove> GetValidMoves(IBoard currentBoard, Point pos)
    {
        throw new System.NotImplementedException();
    }
}