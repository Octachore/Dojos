namespace Visitor.Nodes
{
    public class ValueNode : INode
    {
        public int Value { get; }

        public ValueNode(int value)
        {
            Value = value;
        }

        public ValueNode()
        {
        }

        public T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
    }
}
