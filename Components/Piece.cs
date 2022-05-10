using System.Collections.Generic;
using BossChess.Interfaces;
using Microsoft.Xna.Framework;

namespace BossChess.Components;

public abstract class Piece
{
    public abstract List<IMove> GetRawMoves(IBoard currentBoard, Point pos);

    protected virtual bool AddIfEmpty(List<IMove> currentMoves, IBoard currentBoard, Point pos, int xOff, int yOff)
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

    protected virtual bool CheckIfEmpty(IBoard currentBoard, Point pos, int xOff, int yOff)
    {
        Point toMove = new Point(pos.X+xOff, pos.Y+yOff);
        PrimitivePiece p;
        if (currentBoard.TryGetPieceAt(toMove, out p))
        {
            return (p.Type==PieceType.None);
        }
        return false;
    }

    protected virtual bool AddIfOnEnemy(List<IMove> currentMoves, IBoard currentBoard, Point pos, int xOff, int yOff)
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

    protected virtual bool AddIfNotOnFriend(List<IMove> currentMoves, IBoard currentBoard, Point pos, int xOff, int yOff)
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

    public static int GetValue(PieceType t)
    {
        switch (t)
        {
            case PieceType.Pawn:
                return 1;

            case PieceType.Knight:
            case PieceType.Biship:
                return 3;
            
            case PieceType.Rook:
                return 5;

            case PieceType.Queen:
                return 9;
            
            case PieceType.King:
            case PieceType.None:
            default:
                return 0;
        }
    }
}