using BossChess.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BossChess.Components;

public class Queen: Piece
{
    public override List<IMove> GetRawMoves(IBoard currentBoard, Point pos)
    {
        throw new System.NotImplementedException();
    }

    public override List<IMove> GetValidMoves(IBoard currentBoard, Point pos)
    {
        throw new System.NotImplementedException();
    }
}