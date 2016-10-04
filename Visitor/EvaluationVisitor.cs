using System;
using Visitor.Nodes;
using Visitor.Parsing;

namespace Visitor
{
    public class EvaluationVisitor : IVisitor<int>
    {
        public int Visit(BinaryNode node)
        {
            int left = node.LeftNode.Accept(this);
            int right = node.RightNode.Accept(this);

            switch (node.Type)
            {
                case TokenType.AddOpp:
                    return left + right;
                case TokenType.SubOpp:
                    return left - right;
                case TokenType.MultOpp:
                    return left * right;
                case TokenType.DivOpp:
                    return left / right;
                case TokenType.ModOpp:
                    return left % right;
                default:
                    throw new ArgumentOutOfRangeException($"Expected an operation token type but got {node.Type}");
            }
        }

        public int Visit(ValueNode node) => node.Value;

        public int Visit(UnaryNode node) => node.Type == TokenType.SubOpp ? -node.ChildNode.Accept(this) : node.ChildNode.Accept(this);
    }
}
