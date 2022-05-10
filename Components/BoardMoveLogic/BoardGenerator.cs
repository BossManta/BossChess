using System;
using System.Collections.Generic;
using BossChess.Interfaces;
using Microsoft.Xna.Framework;

namespace BossChess.Components;

public static class BoardGenerator
{
    /// <summary>
    /// Returns immutable board after given move has been made
    /// </summary>
    public static IBoard GenerateNewBoardWithMove(IBoard board, IMove move)
    {
        PrimitivePiece[,] primGrid = (PrimitivePiece[,])board.PrimitivePieceGrid.Clone();
        
        //Removes toremove piece
        if (move.ToRemove is not null)
        {
            primGrid[((Point)move.ToRemove).X, ((Point)move.ToRemove).Y] =  new PrimitivePiece();
        }

        //Actual Move logic (Moving pieces)
        foreach (var m in move.ActualMoves)
        {
            primGrid[m.to.X, m.to.Y] = primGrid[m.from.X, m.from.Y];
            primGrid[m.to.X, m.to.Y].HasMoved = true;
            primGrid[m.from.X, m.from.Y] = new PrimitivePiece();
        }

        //Double Pawn Logic
        Point? doublePawnPos = null;
        if (move.ActualMoves.Count>0)
        {
            var m = move.ActualMoves[0];
            if (primGrid[m.to.X, m.to.Y].Type == PieceType.Pawn)
            {
                if (Math.Abs(m.from.Y-m.to.Y)==2)
                {
                    doublePawnPos = m.to;
                }
            }
        }

        //Add Piece Logic (For Pawn Promo)
        foreach (var v in move.ToAdd)
        {
            primGrid[v.pos.X, v.pos.Y] = new PrimitivePiece(v.ptype, board.isWhitesTurn);
        }

        return new Board(primGrid, !board.isWhitesTurn){ doubleMovePawnPos=doublePawnPos };
    }

    /// <summary>
    /// Returns a list of all possible next board states
    /// </summary>
    public static List<IBoard> GenerateAllValidBoards(IBoard board)
    {
        List<IMove> rawMoves = BoardGenerator.GenerateAllRawMoves(board);
        List<IBoard> boards = new List<IBoard>();

        for (int i=rawMoves.Count-1;i>=0;i--)
        {
            IBoard testBoard = BoardGenerator.GenerateNewBoardWithMove(board, rawMoves[i]);
            if (testBoard.IsKingSafe(board.isWhitesTurn))
            {
                boards.Add(testBoard);
            }
        }

        return boards;
    }

    /// <summary>
    /// Generate a list off all possible valid moves
    /// </summary>
    public static List<IMove> GenerateAllValidMoves(IBoard board)
    {
        List<IMove> rawMoves = BoardGenerator.GenerateAllRawMoves(board);
        List<IMove> validMoves = new List<IMove>();

        for (int i=rawMoves.Count-1;i>=0;i--)
        {
            IBoard testBoard = BoardGenerator.GenerateNewBoardWithMove(board, rawMoves[i]);
            if (testBoard.IsKingSafe(board.isWhitesTurn))
            {
                validMoves.Add(rawMoves[i]);
            }
        }

        return validMoves;
    }

    /// <summary>
    /// Used to generate a move using to points on the board
    /// </summary>
    public static List<IMove> GenerateValidMovesAt(IBoard board, Point pos)
    {
        List<IMove> rawMoves = BoardGenerator.GenerateRawMovesAt(board, pos);

        //TODO Check if safe!
        for (int i=rawMoves.Count-1;i>=0;i--)
        {
            IBoard testBoard = BoardGenerator.GenerateNewBoardWithMove(board, rawMoves[i]);
            if (!testBoard.IsKingSafe(board.isWhitesTurn))
            {
                rawMoves.RemoveAt(i);
            }
        }

        return rawMoves;
    }

    /// <sumamry>
    /// Returns a list of all moves (These moves can be illegal)
    /// </summary>
    private static List<IMove> GenerateAllRawMoves(IBoard board)
    {
        List<IMove> moves = new List<IMove>();

        for (int x=0;x<board.PrimitivePieceGrid.GetLength(0);x++)
        {
            for (int y=0;y<board.PrimitivePieceGrid.GetLength(1);y++)
            {
                Point pos = new Point(x,y);
                if (board.GetPieceAt(pos).IsWhite!=board.isWhitesTurn) continue;
                
                moves.AddRange(BoardGenerator.GenerateRawMovesAt(board, pos));
            }
        }

        return moves;
    }

    /// <summary>
    /// Returns a list of all moves for a piece at a given position (These moves can be illegal)
    /// </summary>
    private static List<IMove> GenerateRawMovesAt(IBoard board, Point pos)
    {
        PieceLogicProvider plp = PieceLogicProvider.GetGlobalInstance();
        return plp.GetMoves(board, pos);
    }
}