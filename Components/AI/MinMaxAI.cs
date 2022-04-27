using System.Collections.Generic;
using System;
using BossChess.Interfaces;

namespace BossChess.Components.AI;

public class MinMaxAI
{
    private IBoard rootBoard;
    private TreeNode rootNode;
    int targetDepth;
    int bestMove;

    public void Init(IBoard initialBoard, int targetDepth)
    {
        rootBoard = initialBoard;
        this.targetDepth = targetDepth;

        InitTree();
    }

    public IMove GetBestMove()
    {
        IMove bMove = rootBoard.GenerateAllValidMoves()[bestMove];
        return bMove;
    }

    private int RecursiveMinMax(IBoard board, TreeNode parent, int depth, int alpha, int beta)
    {
        if (depth<=0)
        {
            return board.Evaluate();
        }

        List<IBoard> childBoards = board.GenerateAllValidBoards();
        int bestIndex = -1;
        int val;

        //Maximizer
        if (board.isWhitesTurn)
        {
            val = int.MinValue;

            for (int i=0;i<childBoards.Count;i++)
            {
                int eval = RecursiveMinMax(childBoards[i], parent, depth-1, alpha, beta);
                if (eval>val)
                {
                    val = eval;
                    bestIndex = i;
                }

                //Alpha beta pruning
                alpha = Math.Max(alpha, eval);
                if (beta<=alpha)
                {
                    break;
                }
            }
        }

        //Minimizer
        else
        {
            val = int.MaxValue;

            for (int i=0;i<childBoards.Count;i++)
            {
                int eval = RecursiveMinMax(childBoards[i], parent, depth-1, alpha, beta);
                if (eval<val)
                {
                    val = eval;
                    bestIndex = i;
                }

                //Alpha beta pruning
                beta = Math.Min(beta, eval);
                if (beta<=alpha)
                {
                    break;
                }
            }
        }

        return val;
    }

    private void InitTree()
    {
        //This first node is kept out of the while loop to allow for possible future parallelization
        //Stack<(IBoard board, TreeNode parent, int depth)> boardFrountier = new Stack<(IBoard, TreeNode, int)>();
        rootNode = new TreeNode();

        List<IBoard> childBoards = rootBoard.GenerateAllValidBoards();

        int val = int.MaxValue;
        int bestIndex = -1;
        for (int i=0;i<childBoards.Count;i++)
        {
            int eval = RecursiveMinMax(childBoards[i], rootNode, targetDepth, int.MinValue, int.MaxValue);
            if (eval<val)
            {
                val = eval;
                bestIndex = i;
            }
        }
        System.Console.WriteLine($"Estimated Stength(Lower = better for bot): {val}");
        bestMove = bestIndex;
    }
}