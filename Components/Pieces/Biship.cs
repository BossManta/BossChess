using BossChess.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BossChess.Components;

public class Biship: Piece
{
    public override List<IMove> GetRawMoves(IBoard currentBoard, Point pos)
    {
        List<IMove> moves =  new List<IMove>();

        for (int i=-1;i<=1;i+=2)
        {
            for (int j=-1;j<=1;j+=2)
            {
                int counter = 1;
                while (IfEmptyAdd(moves, currentBoard, pos, counter*j, counter*i))
                {
                    counter++;
                }
                IfOnEnemyAt(moves, currentBoard, pos, counter*j, counter*i);
            }
        }

        return moves;
    }

    public override List<IMove> GetValidMoves(IBoard currentBoard, Point pos)
    {
        throw new System.NotImplementedException();
    }
}