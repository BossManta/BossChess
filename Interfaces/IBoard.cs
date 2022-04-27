using Microsoft.Xna.Framework;
using BossChess.Components;
using System.Collections.Generic;

namespace BossChess.Interfaces;

public interface IBoard
{

    public bool isWhitesTurn { get; init; }

    public Point Size { get; init; }

    public Point? doubleMovePawnPos { get; init; }

    /// <summary>
    /// Returns immutable board after move has been made
    ///
    /// <para>Note: You can generate a move using the GenerateMove() method.</para>
    /// </summary>
    IBoard GenerateNewBoardWithMove(IMove move);

    List<IBoard> GenerateAllValidBoards();
    List<IMove> GenerateAllValidMoves();

    /// <summary>
    /// Used to generate a move using to points on the board
    /// </summary>
    List<IMove> GenerateValidMovesAt(Point pos);

    List<IMove> GenerateAllRawMoves();
    List<IMove> GenerateRawMovesAt(Point pos);

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