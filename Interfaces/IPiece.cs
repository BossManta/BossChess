using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BossChess.Interfaces
{
    public interface IPiece
    {
        // <summary>
        // Returns a list of moves. These moves do not check if king is in check.
        // <para>Use GetValidMoves() to prevent moving into check</para>
        // </summary>
        List<IMove> GetRawMoves(IBoard currentBoard);

        // <summary>
        // Returns a list of possible moves. These moves prevent moving into check</para>
        // </summary>
        List<IMove> GetValidMoves(IBoard currentBoard);
    }
}