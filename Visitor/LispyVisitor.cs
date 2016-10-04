using System;
using Visitor.Nodes;
using Visitor.Parsing;

namespace Visitor
{
    public class LispyVisitor : IVisitor<string>
    {
        public string Visit(BinaryNode node)
        {
            string opp = GetString(node.Type);
            string left = node.LeftNode.Accept(this);
            string right = node.RightNode.Accept(this);
            return $"[{opp} {left} {right}]";
        }

        public string Visit(ValueNode node) => node.Value.ToString();
        public string Visit(UnaryNode node) => node.Type == TokenType.SubOpp ? $"[- {node.ChildNode.Accept(this)}]" : node.ChildNode.Accept(this);

        private string GetString(TokenType type)
        {
            switch (type)
            {
                case TokenType.AddOpp:
                    return "+";
                case TokenType.SubOpp:
                    return "-";
                case TokenType.MultOpp:
                    return "*";
                case TokenType.DivOpp:
                    return "/";
                case TokenType.ModOpp:
                    return "%";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
