using Visitor.Nodes;

namespace Visitor.Helpers
{
    public static class VisitorExtensions
    {
        public static T Visit<T>(this IVisitor<T> visitor, INode node) => node.Accept(visitor);
    }
}
