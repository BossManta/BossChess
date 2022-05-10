using System;
using System.Collections.Generic;
using BossChess.Interfaces;
using Microsoft.Xna.Framework;

namespace BossChess.Components;

public record Board : IBoard
{
    public bool isWhitesTurn { get; init; } = true;

    public Point Size { get; init; }

    public Point? doubleMovePawnPos { get; init; } = null;
    public PrimitivePiece[,] PrimitivePieceGrid { get; init; }

    public Board(PrimitivePiece[,] grid, bool isWhitesTurn)
    {
        PrimitivePieceGrid = grid;
        this.isWhitesTurn = isWhitesTurn;
    }

    public Board()
    {
        PrimitivePiece[,] newPieceGrid = new PrimitivePiece[8,8];
        Size = new Point(8,8);

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
            newPieceGrid[3,i] = new PrimitivePiece(PieceType.Queen, isWhite);

            //Kings
            newPieceGrid[4,i] = new PrimitivePiece(PieceType.King, isWhite);
        }

        PrimitivePieceGrid = newPieceGrid;
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

    public bool IsPosSafe(bool isWhite, Point pos)
    {
        //Pawn
        int pawnDangerDirection = isWhite?-1:1;
        for (int i=-1;i<=1;i+=2)
        {
            if (CheckIfPieceIsInDangerZone(PieceType.Pawn, isWhite, pos, i, pawnDangerDirection))
            {
                return false;
            }
        }

        //Knight
        for (int leftRightSetter=0;leftRightSetter<=1;leftRightSetter++)
        {
            int left = leftRightSetter+1;
            int right = leftRightSetter-2;

            for (int i=-1;i<=1;i+=2)
            {
                for (int j=-1;j<=1;j+=2)
                {
                    if (CheckIfPieceIsInDangerZone(PieceType.Knight, isWhite, pos, left*i, right*j))
                    {
                        return false;
                    }
                }
            }
        }

        //Biship, Rook, Queen
        for (int i=-1;i<=1;i+=2)
        {
            int counter = 1;
            while (CheckIfEmpty(pos, 0, counter*i))
            {
                counter++;
            }
            if (CheckIfPieceIsInDangerZone(PieceType.Rook, isWhite, pos, 0, counter*i)) return false;
            if (CheckIfPieceIsInDangerZone(PieceType.Queen, isWhite, pos, 0, counter*i)) return false;

            counter = 1;
            while (CheckIfEmpty(pos, counter*i, 0))
            {
                counter++;
            }
            if (CheckIfPieceIsInDangerZone(PieceType.Rook, isWhite, pos, counter*i, 0)) return false;
            if (CheckIfPieceIsInDangerZone(PieceType.Queen, isWhite, pos, counter*i, 0)) return false;

            for (int j=-1;j<=1;j+=2)
            {
                counter = 1;
                while (CheckIfEmpty(pos, counter*j, counter*i))
                {
                    counter++;
                }
                if (CheckIfPieceIsInDangerZone(PieceType.Biship, isWhite, pos, counter*j, counter*i)) return false;
                if (CheckIfPieceIsInDangerZone(PieceType.Queen, isWhite, pos, counter*j, counter*i)) return false;
            }
        }

        //King
        for (int xOff=-1;xOff<=1;xOff++)
        {
            for (int yOff=-1;yOff<=1;yOff++)
            {
                if (xOff==0 && yOff==0) continue;

                if (CheckIfPieceIsInDangerZone(PieceType.King, isWhite, pos, xOff, yOff)) return false;
            }
        }

        return true;
    }

    public bool IsKingSafe(bool isWhiteKing)
    {
        //ToDo Optimize finding king
        Point kingPos = new Point(-1,-1);
        for (int x=0;x<PrimitivePieceGrid.GetLength(0);x++)
        {
            for (int y=0;y<PrimitivePieceGrid.GetLength(1);y++)
            {
                if (PrimitivePieceGrid[x,y].Type==PieceType.King && PrimitivePieceGrid[x,y].IsWhite==isWhiteKing)
                {
                    kingPos = new Point(x,y);
                }
            }
        }

        return IsPosSafe(isWhiteKing, kingPos);       
    }

    //This is strictly for the IsPosSafe() method.
    private bool CheckIfPieceIsInDangerZone(PieceType pieceType, bool isWhiteKing, Point pos, int x, int y)
    {
        PrimitivePiece p;
        if (TryGetPieceAt(new Point(pos.X+x,pos.Y+y), out p))
        {
            if (p.Type==pieceType && p.IsWhite!=isWhiteKing)
            {
                return true;
            }
        }
        return false;
    }  
    //This is also strictly for the IsPosSafe() method.
    private bool CheckIfEmpty(Point pos, int x, int y)
    {
        PrimitivePiece p;
        if (TryGetPieceAt(new Point(pos.X+x,pos.Y+y), out p))
        {
            if (p.Type==PieceType.None)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsCheckMated(bool isWhite)
    {
        if (IsKingSafe(isWhite))
        {
            var possibleMoves = BoardGenerator.GenerateAllValidBoards(this);
            return possibleMoves.Count==0;
        }
        return false;
    }

    // public float Evaluate()
    // {
    //     PieceLogicProvider plp = PieceLogicProvider.GetGlobalInstance();
    //     int val = 0;
    //     for (int x=0;x<PrimitivePieceGrid.GetLength(0);x++)
    //     {
    //         for (int y=0;y<PrimitivePieceGrid.GetLength(1);y++)
    //         {
    //             PrimitivePiece p = PrimitivePieceGrid[x,y];
    //             if (p.Type!=PieceType.None)
    //             {
    //                 int pVal;
    //                 if (p.IsWhite)
    //                 {
    //                     pVal = Piece.GetValue(p.Type);
    //                 }
    //                 else
    //                 {
    //                     pVal = -Piece.GetValue(p.Type);
    //                 }
    //                 val+=pVal;
    //             }
    //         }
    //     }
    //     return val;
    // }
}