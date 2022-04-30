using Microsoft.Xna.Framework;
using BossChess.Components;
using System.Collections.Generic;

namespace BossChess.Interfaces;

public interface IBoard
{

    public bool isWhitesTurn { get; init; }

    public Point Size { get; init; }

    public Point? doubleMovePawnPos { get; init; }

    public PrimitivePiece[,] PrimitivePieceGrid { get; init; }

    // <summary>
    // Returns primitivePiece from board
    // </summary>
    PrimitivePiece GetPieceAt(Point p);

    // <summary>
    // Tries to get piece if on board. 
    // If given point is off the board the method returns false.
    // If given point is empty returns true with null piece.
    bool TryGetPieceAt(Point p, out PrimitivePiece piece);

    int Evaluate();

    bool IsPosSafe(bool isWhite, Point pos);
    bool IsKingSafe(bool isWhiteKing);
}