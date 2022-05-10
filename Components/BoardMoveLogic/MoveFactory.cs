using BossChess.Interfaces;
using Microsoft.Xna.Framework;

namespace BossChess.Components;

public class MoveFactory
{
    public static IMove MakeMove()
    {
        return new Move();
    }

    public static IMove MakeMove(Point from, Point to)
    {
        return new Move(from, to);
    }
}