using BossChess.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BossChess.Components;

public class Knight: Piece
{
    public override List<IMove> GetRawMoves(IBoard currentBoard, Point pos)
    {
        List<IMove> moves = new List<IMove>();

        //Make this better:
        IfNotOnFriend(moves, currentBoard, pos, 1, 2);
        IfNotOnFriend(moves, currentBoard, pos, 1, -2);
        IfNotOnFriend(moves, currentBoard, pos, -1, 2);
        IfNotOnFriend(moves, currentBoard, pos, -1, -2);
        IfNotOnFriend(moves, currentBoard, pos, 2, 1);
        IfNotOnFriend(moves, currentBoard, pos, 2, -1);
        IfNotOnFriend(moves, currentBoard, pos, -2, 1);
        IfNotOnFriend(moves, currentBoard, pos, -2, -1);



        return moves;
    }

    public override List<IMove> GetValidMoves(IBoard currentBoard, Point pos)
    {
        throw new System.NotImplementedException();
    }
}