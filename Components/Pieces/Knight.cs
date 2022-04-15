using BossChess.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BossChess.Components;

public class Knight: Piece
{
    public override List<IMove> GetRawMoves(IBoard currentBoard, Point pos)
    {
        List<IMove> moves = new List<IMove>();

        for (int leftRightSetter=0;leftRightSetter<=1;leftRightSetter++)
        {
            int left = leftRightSetter+1;
            int right = leftRightSetter-2;

            for (int i=-1;i<=1;i+=2)
            {
                for (int j=-1;j<=1;j+=2)
                {
                    IfNotOnFriend(moves, currentBoard, pos, left*i, right*j);
                }
            }
        }



        return moves;
    }

    public override List<IMove> GetValidMoves(IBoard currentBoard, Point pos)
    {
        throw new System.NotImplementedException();
    }
}