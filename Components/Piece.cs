using System.Collections.Generic;
using BossChess.Interfaces;
using Microsoft.Xna.Framework;

namespace BossChess.Components;

public abstract class Piece
{
    public abstract List<IMove> GetRawMoves(IBoard currentBoard, Point pos);

    public abstract List<IMove> GetValidMoves(IBoard currentBoard, Point pos);

    protected virtual bool IfEmptyAdd(List<IMove> currentMoves, IBoard currentBoard, Point pos, int xOff, int yOff)
    {
        Point toMove = new Point(pos.X+xOff, pos.Y+yOff);
        PrimitivePiece p;
        if (currentBoard.TryGetPieceAt(toMove, out p))
        {
            if (p.Type==PieceType.None)
            {
                currentMoves.Add(MoveFactory.MakeMove(pos,toMove));
                return true;
            }
        }
        return false;
    }

    protected virtual bool IfOnEnemyAt(List<IMove> currentMoves, IBoard currentBoard, Point pos, int xOff, int yOff)
    {
        PrimitivePiece currentPiece = currentBoard.GetPieceAt(pos);
        Point toMove = new Point(pos.X+xOff, pos.Y+yOff);
        PrimitivePiece p;
        if (currentBoard.TryGetPieceAt(toMove, out p))
        {
            if (p.IsWhite==currentPiece.IsWhite || p.Type==PieceType.None) return false;

            currentMoves.Add(MoveFactory.MakeMove(pos,toMove));
            return true;
        }
        return false;
    }

    protected virtual bool IfNotOnFriend(List<IMove> currentMoves, IBoard currentBoard, Point pos, int xOff, int yOff)
    {
        PrimitivePiece currentPiece = currentBoard.GetPieceAt(pos);
        Point toMove = new Point(pos.X+xOff, pos.Y+yOff);
        PrimitivePiece p;
        if (currentBoard.TryGetPieceAt(toMove, out p))
        {
            if (p.IsWhite==currentPiece.IsWhite && p.Type!=PieceType.None) return false;

            currentMoves.Add(MoveFactory.MakeMove(pos,toMove));
            return true;
        }
        return false;
    }

    protected virtual bool IsPosEmptyAndSafe(IBoard currentBoard, Point pos, bool isWhite)
    {
        PrimitivePiece p;
        if (currentBoard.TryGetPieceAt(pos, out p))
        {
            return (p.Type==PieceType.None && currentBoard.IsPosSafe(isWhite, pos));
        }
        return false;
    }
}