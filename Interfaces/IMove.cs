using System.Collections.Generic;
using BossChess.Components;
using Microsoft.Xna.Framework;

namespace BossChess.Interfaces
{
    public interface IMove
    {
        public List<(Point from, Point to)> ActualMoves { get; set; }
        public Point ToRemove { get; set; }
        public List<(Point pos, PieceType ptype)> ToAdd { get; set; }
    }
}