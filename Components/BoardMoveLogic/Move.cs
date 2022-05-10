using System.Collections.Generic;
using BossChess.Interfaces;
using Microsoft.Xna.Framework;

namespace BossChess.Components;

public class Move: IMove
{
    public List<(Point from, Point to)> ActualMoves { get; set; } = new List<(Point from, Point to)>();
    public Point? ToRemove { get; set; } = null;
    public List<(Point pos, PieceType ptype)> ToAdd { get; set; } = new List<(Point pos, PieceType ptype)>();

    public Move(){}

    public Move(Point from, Point to)
    {
        ActualMoves.Add((from, to));
    }
}