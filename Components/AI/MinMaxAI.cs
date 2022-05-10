using System.Collections.Generic;
using System;
using BossChess.Interfaces;

namespace BossChess.Components.AI;

public class MinMaxAI
{
    private IBoard rootBoard;
    private BoardEvaluator boardEvaluator;
    int targetDepth;
    int bestMove;

    public void Init(IBoard initialBoard, int targetDepth, BoardEvaluator boardEvaluator)
    {
        rootBoard = initialBoard;
        this.targetDepth = targetDepth;
        this.boardEvaluator = boardEvaluator;

        InitTree();
    }

    public IMove GetBestMove()
    {
        IMove bMove = BoardGenerator.GenerateAllValidMoves(rootBoard)[bestMove];
        return bMove;
    }

    private float RecursiveMinMax(IBoard board, int depth, float alpha, float beta)
    {
        if (depth<=0)
        {
            return boardEvaluator.GetValue(board);
        }

        List<IBoard> childBoards = BoardGenerator.GenerateAllValidBoards(board);
        int bestIndex = -1;
        float val;

        //Checks for checkmate or stalemate
        if (childBoards.Count==0)
        {
            //Checkmate
            if (board.IsCheckMated(board.isWhitesTurn))
            {
                return board.isWhitesTurn?float.MaxValue:float.MinValue;
            }

            //Stalemate
            return 0;
        }

        //Maximizer
        if (board.isWhitesTurn)
        {
            val = float.MinValue;

            for (int i=0;i<childBoards.Count;i++)
            {
                float eval = RecursiveMinMax(childBoards[i], depth-1, alpha, beta);
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
            val = float.MaxValue;

            for (int i=0;i<childBoards.Count;i++)
            {
                float eval = RecursiveMinMax(childBoards[i], depth-1, alpha, beta);
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
        List<IBoard> childBoards = BoardGenerator.GenerateAllValidBoards(rootBoard);
        float val;
        int bestIndex;
        if (!rootBoard.isWhitesTurn)
        {
            val = int.MaxValue;
            bestIndex = -1;
            for (int i=0;i<childBoards.Count;i++)
            {
                float eval = RecursiveMinMax(childBoards[i], targetDepth-1, int.MinValue, int.MaxValue);
                if (eval<val)
                {
                    val = eval;
                    bestIndex = i;
                }
            }
        }
        else
        {
            val = int.MinValue;
            bestIndex = -1;
            for (int i=0;i<childBoards.Count;i++)
            {
                float eval = RecursiveMinMax(childBoards[i], targetDepth-1, int.MinValue, int.MaxValue);
                if (eval>val)
                {
                    val = eval;
                    bestIndex = i;
                }
            }
        }
        System.Console.WriteLine($"Current Strength: {boardEvaluator.GetValue(rootBoard)}");
        System.Console.WriteLine($"Estimated Stength(Lower = better for bot): {val}");
        bestMove = bestIndex;
    }
}