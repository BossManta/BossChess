using BossChess.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BossChess.Components;

public class Rook: Piece
{
    public override List<IMove> GetRawMoves(IBoard currentBoard, Point pos)
    {
        List<IMove> moves =  new List<IMove>();

        for (int i=-1;i<=1;i+=2)
        {
            int counter = 1;
            while (AddIfEmpty(moves, currentBoard, pos, 0, counter*i))
            {
                counter++;
            }
            AddIfOnEnemy(moves, currentBoard, pos, 0, counter*i);

            counter = 1;
            while (AddIfEmpty(moves, currentBoard, pos, counter*i, 0))
            {
                counter++;
            }
            AddIfOnEnemy(moves, currentBoard, pos, counter*i, 0);
        }

        return moves;
    }
}