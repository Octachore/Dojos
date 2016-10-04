using System;
using Visitor.Parsing;

namespace Visitor.Exceptions
{
    [Serializable]
    internal class NoMatchException : Exception
    {
        public override string Message { get; }

        public NoMatchException(TokenType actualType, params TokenType[] expectedTypes)
        {
            string expected = $"{{{string.Join(" ; ", expectedTypes)}}}";
            Message = $"Got {actualType} but expected one of {expected}.";
        }
    }
}