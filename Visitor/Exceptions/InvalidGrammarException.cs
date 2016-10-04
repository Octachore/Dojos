using System;
using Visitor.Parsing;

namespace Visitor.Exceptions
{
    [Serializable]
    internal class InvalidGrammarException : Exception
    {
        public override string Message { get; }

        public InvalidGrammarException(Token token)
        {
            Message = $"Invalid grammar. Misplaced token: {{Type: {token.Type} ; Value: {token.Value}}}";
        }
    }
}