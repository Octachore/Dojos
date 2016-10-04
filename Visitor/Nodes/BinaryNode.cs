using Utils;
using Visitor.Parsing;

namespace Visitor.Nodes
{
    public class BinaryNode : INode
    {
        public INode LeftNode { get; }

        public INode RightNode { get; }

        public TokenType Type { get; }

        public BinaryNode(INode leftNode, INode rightNode, TokenType type)
        {
            Guard.In(type, TokenType.AddOpp, TokenType.SubOpp, TokenType.MultOpp, TokenType.DivOpp, TokenType.ModOpp);

            LeftNode = leftNode;
            RightNode = rightNode;
            Type = type;
        }

        public BinaryNode()
        {
        }

        public T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
    }
}
