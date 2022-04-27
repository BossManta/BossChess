using BossChess.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace BossChess.Components;

public class Pawn: Piece
{
    public override List<IMove> GetRawMoves(IBoard currentBoard, Point pos)
    {
        List<IMove> moves = new List<IMove>();

        PrimitivePiece currentPiece = currentBoard.GetPieceAt(pos);

        int direction = currentPiece.IsWhite?-1:1;

        //Normal move
        AddIfEmpty(moves, currentBoard, pos, 0, direction);

        //Double first move
        if (!currentPiece.HasMoved)
        {
            if (CheckIfEmpty(currentBoard, pos, 0, direction))
            {
                AddIfEmpty(moves, currentBoard, pos, 0, direction*2);
            }
        }

        //Capture moves
        AddIfOnEnemy(moves, currentBoard, pos, -1, direction);
        AddIfOnEnemy(moves, currentBoard, pos, 1, direction);


        //Enpass
        if (currentBoard.doubleMovePawnPos is not null)
        {
            Point enpassPawn = (Point)currentBoard.doubleMovePawnPos;
            if (enpassPawn.Y == pos.Y)
            {
                if (Math.Abs(enpassPawn.X-pos.X)==1)
                {
                    IMove enpassMove = MoveFactory.MakeMove(pos, new Point(enpassPawn.X, enpassPawn.Y+direction));
                    enpassMove.ToRemove = enpassPawn;
                    moves.Add(enpassMove);
                }
            }
        }

        List<IMove> newPromoMoves = new List<IMove>();
        foreach (IMove m in moves)
        {
            int promoYPos = currentPiece.IsWhite?0:7;//currentBoard.Size.Y-5;
            Point targetPos = m.ActualMoves[0].to;

            if (targetPos.Y==promoYPos)
            {
                m.ToAdd.Add((targetPos, PieceType.Queen));
                
                PieceType[] possiblePromos = {PieceType.Knight, PieceType.Rook, PieceType.Biship};
                
                foreach (PieceType t in possiblePromos)
                {
                    IMove newMove = MoveFactory.MakeMove(pos, targetPos);
                    newMove.ToAdd.Add((targetPos, t));
                    newPromoMoves.Add(newMove);
                }
            }
        }

        moves.AddRange(newPromoMoves);

        return moves;
    }
}