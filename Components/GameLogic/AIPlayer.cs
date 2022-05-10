using System.Threading;
using System.Threading.Tasks;
using BossChess.Components.AI;

namespace BossChess.Components;

public class AIPlayer : AbstractPlayer
{
    private MinMaxAI ai = new MinMaxAI();
    private int targetDepth;

    public AIPlayer(int targetDepth=4)
    {
        this.targetDepth = targetDepth;
    }

    private void MakeMove()
    {
        BoardEvaluator be = new BoardEvaluator();
        ai.Init(gm.currentBoard, targetDepth, be);
        SubmitMove(ai.GetBestMove());
    }

    public override void AuthorizeToMakeMove()
    {
        Task t1 = new Task(MakeMove);
        t1.Start();
    }
}