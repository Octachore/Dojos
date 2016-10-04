using Visitor.Nodes;

namespace Visitor
{
    public interface IVisitor<out T>
    {
        T Visit(BinaryNode node);

        T Visit(ValueNode node);

        T Visit(UnaryNode node);
    }
}
