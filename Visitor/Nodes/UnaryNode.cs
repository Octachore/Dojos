using Utils;
using Visitor.Parsing;

namespace Visitor.Nodes
{
    public class UnaryNode : INode
    {
        public TokenType Type { get; }

        public INode ChildNode { get; }

        public UnaryNode(TokenType type, INode childNode)
        {
            Guard.In(type, TokenType.SubOpp, TokenType.AddOpp);
            Type = type;
            ChildNode = childNode;
        }

        public T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
    }
}
