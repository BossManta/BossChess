using System;
using System.Collections.Generic;
using BossChess.Interfaces;
using Microsoft.Xna.Framework;

namespace BossChess.Components;

public record Board : IBoard
{
    public bool isWhitesTurn { get; init; } = true;
    private PrimitivePiece[,] PrimitivePieceGrid { get; init; }

    public Board(PrimitivePiece[,] grid, bool isWhitesTurn)
    {
        PrimitivePieceGrid = grid;
        this.isWhitesTurn = isWhitesTurn;
    }

    public Board()
    {
        PrimitivePiece[,] newPieceGrid = new PrimitivePiece[8,8];

        //Place Pawns
        for (int i=0;i<newPieceGrid.GetLength(0);i++)
        {
            //Black Pawn
            newPieceGrid[i,1] = new PrimitivePiece(PieceType.Pawn, false);

            //White Pawn
            newPieceGrid[i,6] = new PrimitivePiece(PieceType.Pawn, true);
        }

        for (int i=0; i<=7; i+=7)
        {
            bool isWhite = i==0?false:true;

            //Rooks
            newPieceGrid[0,i] = new PrimitivePiece(PieceType.Rook, isWhite);
            newPieceGrid[7,i] = new PrimitivePiece(PieceType.Rook, isWhite);

            //Knights
            newPieceGrid[1,i] = new PrimitivePiece(PieceType.Knight, isWhite);
            newPieceGrid[6,i] = new PrimitivePiece(PieceType.Knight, isWhite);

            //Biships
            newPieceGrid[2,i] = new PrimitivePiece(PieceType.Biship, isWhite);
            newPieceGrid[5,i] = new PrimitivePiece(PieceType.Biship, isWhite);

            //Queens
            newPieceGrid[4,i] = new PrimitivePiece(PieceType.Queen, isWhite);

            //Kings
            newPieceGrid[3,i] = new PrimitivePiece(PieceType.King, isWhite);
        }

        PrimitivePieceGrid = newPieceGrid;
    }

    public List<IMove> GenerateValidMovesAt(Point pos)
    {
        throw new NotImplementedException();
    }

    public List<IMove> GenerateRawMovesAt(Point pos)
    {
        PieceLogicProvider plp = PieceLogicProvider.GetGlobalInstance();
        return plp.GetMoves(this, pos);
    }

    public PrimitivePiece GetPieceAt(Point p)
    {
        if (p.X>=PrimitivePieceGrid.GetLength(0) || p.X<0 || p.Y>=PrimitivePieceGrid.GetLength(1) || p.Y<0)
        {
            throw new ArgumentOutOfRangeException("Point given is not on the board");
        }
        return PrimitivePieceGrid[p.X,p.Y];
    }

    public bool TryGetPieceAt(Point p, out PrimitivePiece piece)
    {
        if (p.X>=PrimitivePieceGrid.GetLength(0) || p.X<0 || p.Y>=PrimitivePieceGrid.GetLength(1) || p.Y<0)
        {
            piece = new PrimitivePiece();
            return false;
        }
        else
        {
            piece = PrimitivePieceGrid[p.X,p.Y];
            return true;
        }
    }

    public bool IsKingSafe(bool isWhiteKing)
    {
        throw new System.NotImplementedException();
    }  

    //TODO (Change Move to IMove)
    public IBoard GenerateNewBoardWithMove(IMove move)
    {
        PrimitivePiece[,] primGrid = (PrimitivePiece[,])PrimitivePieceGrid.Clone();
        foreach (var m in move.ActualMoves)
        {
            primGrid[m.to.X, m.to.Y] = primGrid[m.from.X, m.from.Y];
            primGrid[m.to.X, m.to.Y].HasMoved = true;
            primGrid[m.from.X, m.from.Y] = new PrimitivePiece();
        }

        return new Board(primGrid, !this.isWhitesTurn);
    }
}
