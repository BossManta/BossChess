using System.Threading;
using System.Threading.Tasks;
using BossChess.Components.AI;

namespace BossChess.Components;

public class AIPlayer : AbstractPlayer
{
    private MinMaxAI ai = new MinMaxAI();

    private void MakeMove()
    {
        BoardEvaluator be = new BoardEvaluator();
        ai.Init(gm.currentBoard, 4, be);
        SubmitMove(ai.GetBestMove());
    }

    public override void AuthorizeToMakeMove()
    {
        Task t1 = new Task(MakeMove);
        t1.Start();
    }
}