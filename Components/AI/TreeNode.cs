using System.Collections.Generic;
using BossChess.Interfaces;

namespace BossChess.Components.AI;

public class TreeNode
{
    public TreeNode Parent { get; set; }
    public int Value { get; set; }
    public List<TreeNode> Children { get; set; }
    public IBoard Board { get; set; }
}