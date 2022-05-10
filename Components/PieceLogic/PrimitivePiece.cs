namespace BossChess.Components;

public struct PrimitivePiece
{
    public PieceType Type { get; set; }
    public bool IsWhite { get; set; }

    //For pawns and castling pieces (King & Rook)
    public bool HasMoved { get; set; }

    public PrimitivePiece(PieceType t, bool isWhite)
    {
        Type = t;
        IsWhite = isWhite;

        HasMoved = false;
    }
}