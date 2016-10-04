namespace Visitor.Nodes
{
    public interface INode
    {
        T Accept<T>(IVisitor<T> visitor);
    }
}
