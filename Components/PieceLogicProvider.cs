using System.Collections.Generic;
using BossChess.Interfaces;
using Microsoft.Xna.Framework;

namespace BossChess.Components;

public class PieceLogicProvider
{
    private Dictionary<PieceType, Piece> pieceDict =  new Dictionary<PieceType, Piece>();
    private static PieceLogicProvider GlobalInstance = new PieceLogicProvider();

    public PieceLogicProvider()
    {
        pieceDict.Add(PieceType.Pawn, new Pawn());
        pieceDict.Add(PieceType.Rook, new Rook());
        pieceDict.Add(PieceType.Knight, new Knight());
        pieceDict.Add(PieceType.Biship, new Biship());
        pieceDict.Add(PieceType.Queen, new Queen());
        pieceDict.Add(PieceType.King, new King());
    }

    public static PieceLogicProvider GetGlobalInstance()
    {
        return GlobalInstance;
    }

    public List<IMove> GetMoves(IBoard board, Point pos)
    {
        PrimitivePiece toMovePiece = board.GetPieceAt(pos);
        if (toMovePiece.Type==PieceType.None)
        {
            return new List<IMove>();
        }

        return pieceDict[toMovePiece.Type].GetRawMoves(board, pos);
    }
}