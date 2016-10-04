using System.Linq;
using System.Text;
using Visitor.Exceptions;

namespace Visitor.Parsing
{
    public class Lexer
    {
        protected readonly string Input;
        private int _pos;

        public Token CurrentToken { get; protected set; }

        public bool IsEnd => _pos >= Input.Length;

        public bool IsNumber => char.IsNumber(Peek());

        public bool IsWhiteSpace => char.IsWhiteSpace(Peek());

        public Lexer(string input)
        {
            Input = input;
            _pos = 0;
        }

        public char Consume() => Input[_pos++];

        public bool IsMatching(params TokenType[] types) => types.Contains(CurrentToken.Type);

        public void Match(params TokenType[] types)
        {
            if (!types.Contains(CurrentToken.Type)) throw new NoMatchException(CurrentToken.Type, types);
            NextToken();
        }

        public Token NextToken()
        {
            while (IsEnd || IsWhiteSpace)
            {
                if (IsEnd) return new Token(TokenType.EOI);
                if (IsWhiteSpace) HandleWhiteSpaces();
            }

            return DetermineToken();
        }

        public char Peek() => Input[_pos];
        protected Token DetermineToken()
        {
            char c = Peek();
            switch (c)
            {
                case '(':
                    CurrentToken = new Token(TokenType.OpenPar);
                    break;
                case ')':
                    CurrentToken = new Token(TokenType.ClosePar);
                    break;
                case '-':
                    CurrentToken = new Token(TokenType.SubOpp);
                    break;
                case '+':
                    CurrentToken = new Token(TokenType.AddOpp);
                    break;
                case '*':
                    CurrentToken = new Token(TokenType.MultOpp);
                    break;
                case '/':
                    CurrentToken = new Token(TokenType.DivOpp);
                    break;
                case '%':
                    CurrentToken = new Token(TokenType.ModOpp);
                    break;
                default:
                    if (IsNumber) return CurrentToken = Number();
                    throw new LexiconException($"Unrecognized token: {c}");
            }
            Consume();
            return CurrentToken;
        }

        protected void HandleWhiteSpaces()
        {
            do
            {
                Consume();
            } while (!IsEnd && IsWhiteSpace);
        }
        private Token Number()
        {
            var builder = new StringBuilder();
            do
            {
                builder.Append(Peek());
                Consume();
            } while (!IsEnd && IsNumber);
            return new Token(TokenType.Number, builder.ToString());
        }
    }
}
