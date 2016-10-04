using Visitor.Exceptions;
using Visitor.Nodes;

namespace Visitor.Parsing
{
    public class Parser
    {
        private readonly Lexer _lexer;

        public Parser(Lexer lexer)
        {
            _lexer = lexer;
        }

        public INode Parse()
        {
            _lexer.NextToken();
            return Expression();
        }

        private INode Expression()
        {
            INode node = Term();
            while (_lexer.IsMatching(TokenType.AddOpp, TokenType.SubOpp))
            {
                TokenType oppType = _lexer.CurrentToken.Type;
                _lexer.Match(TokenType.AddOpp, TokenType.SubOpp);
                node = new BinaryNode(node, Term(), oppType);
            }
            return node;
        }

        private INode Term()
        {
            INode node = Factor();
            while (_lexer.IsMatching(TokenType.MultOpp, TokenType.DivOpp, TokenType.ModOpp))
            {
                TokenType oppType = _lexer.CurrentToken.Type;
                _lexer.Match(TokenType.MultOpp, TokenType.DivOpp, TokenType.ModOpp);
                node = new BinaryNode(node, Factor(), oppType);
            }
            return node;
        }

        private INode Factor()
        {
            bool isMinus = false;
            if (_lexer.IsMatching(TokenType.SubOpp))
            {
                isMinus = true;
                _lexer.NextToken();
            }
            INode node = PositiveFactor();
            if (isMinus) node = new UnaryNode(TokenType.SubOpp, node);
            return node;
        }

        private INode PositiveFactor()
        {
            INode node;
            if (_lexer.IsMatching(TokenType.OpenPar))
            {
                _lexer.Match(TokenType.OpenPar);
                node = Expression();
                _lexer.Match(TokenType.ClosePar);
            }
            else if (_lexer.IsMatching(TokenType.Number))
            {
                node = new ValueNode(int.Parse(_lexer.CurrentToken.Value));
                _lexer.Match(TokenType.Number);
            }
            else
            {
                throw new InvalidGrammarException(_lexer.CurrentToken);
            }
            return node;
        }
    }
}
