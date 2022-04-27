using System.Collections.Generic;
using BossChess.Interfaces;
using Microsoft.Xna.Framework;

namespace BossChess.Components;

public class PieceLogicProvider
{
    public Dictionary<PieceType, Piece> PieceDict {get;} =  new Dictionary<PieceType, Piece>();
    private static PieceLogicProvider GlobalInstance = new PieceLogicProvider();

    public PieceLogicProvider()
    {
        PieceDict.Add(PieceType.Pawn, new Pawn());
        PieceDict.Add(PieceType.Rook, new Rook());
        PieceDict.Add(PieceType.Knight, new Knight());
        PieceDict.Add(PieceType.Biship, new Biship());
        PieceDict.Add(PieceType.Queen, new Queen());
        PieceDict.Add(PieceType.King, new King());
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

        return PieceDict[toMovePiece.Type].GetRawMoves(board, pos);
    }
}