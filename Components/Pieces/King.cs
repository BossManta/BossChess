using BossChess.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BossChess.Components;

public class King: Piece
{
    public override List<IMove> GetRawMoves(IBoard currentBoard, Point pos)
    {
        List<IMove> moves =  new List<IMove>();

        for (int i=-1;i<=1;i+=2)
        {
            AddIfNotOnFriend(moves, currentBoard, pos, 0, i);
            AddIfNotOnFriend(moves, currentBoard, pos, i, 0);

            for (int j=-1;j<=1;j+=2)
            {
                AddIfNotOnFriend(moves, currentBoard, pos, j, i);
            }
        }

        PrimitivePiece king = currentBoard.GetPieceAt(pos);

        if (!king.HasMoved)
        {
            //Right Rook
            PrimitivePiece rRook = currentBoard.GetPieceAt(new Point(pos.X+3,pos.Y));
            if (rRook.Type==PieceType.Rook && !rRook.HasMoved)
            {
                if (IsPosEmptyAndSafe(currentBoard, new Point(pos.X+1,pos.Y), king.IsWhite) && IsPosEmptyAndSafe(currentBoard, new Point(pos.X+2,pos.Y) , king.IsWhite))
                {
                    IMove m = MoveFactory.MakeMove(pos, new Point(pos.X+2,pos.Y));
                    m.ActualMoves.Add((new Point(pos.X+3, pos.Y),new Point(pos.X+1,pos.Y)));
                    moves.Add(m);
                }
            }


            //Left Rook
            PrimitivePiece lRook = currentBoard.GetPieceAt(new Point(pos.X-4,pos.Y));
            if (rRook.Type==PieceType.Rook && !rRook.HasMoved)
            {
                if (IsPosEmptyAndSafe(currentBoard, new Point(pos.X-1,pos.Y), king.IsWhite) && IsPosEmptyAndSafe(currentBoard, new Point(pos.X-2,pos.Y) , king.IsWhite) && IsPosEmptyAndSafe(currentBoard, new Point(pos.X-3,pos.Y) , king.IsWhite))
                {
                    IMove m = MoveFactory.MakeMove(pos, new Point(pos.X-2,pos.Y));
                    m.ActualMoves.Add((new Point(pos.X-4, pos.Y),new Point(pos.X-1,pos.Y)));
                    moves.Add(m);
                }
            }
        }

        return moves;
    }

    public override int GetValue()
    {
        return 100;
    }
}