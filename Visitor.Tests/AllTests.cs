using System.Collections.Generic;
using NUnit.Framework;
using Visitor.Helpers;
using Visitor.Nodes;
using Visitor.Parsing;

namespace Visitor.Tests
{
    public class AllTests
    {
        [TestCase("(", TokenType.OpenPar, null)]
        [TestCase(")", TokenType.ClosePar, null)]
        [TestCase("+", TokenType.AddOpp, null)]
        [TestCase("-", TokenType.SubOpp, null)]
        [TestCase("*", TokenType.MultOpp, null)]
        [TestCase("/", TokenType.DivOpp, null)]
        [TestCase("%", TokenType.ModOpp, null)]
        [TestCase("1", TokenType.Number, "1")]
        [TestCase("10", TokenType.Number, "10")]
        [TestCase("", TokenType.EOI, null)]
        public void LexerReturnsRightTokens(string input, TokenType type, string value)
        {
            var lexer = new Lexer(input);
            Token token = lexer.NextToken();
            Assert.That(token.Type, Is.EqualTo(type));
            Assert.That(token.Value, Is.EqualTo(value));
        }

        [Test]
        public void LexerCanTokenize()
        {
            var lexer = new Lexer("1+3 - (6%2) / 14");
            var expected = new List<Token>
                           {
                               new Token(TokenType.Number, "1"),
                               new Token(TokenType.AddOpp, null),
                               new Token(TokenType.Number, "3"),
                               new Token(TokenType.SubOpp, null),
                               new Token(TokenType.OpenPar, null),
                               new Token(TokenType.Number, "6"),
                               new Token(TokenType.ModOpp, null),
                               new Token(TokenType.Number, "2"),
                               new Token(TokenType.ClosePar, null),
                               new Token(TokenType.DivOpp, null),
                               new Token(TokenType.Number, "14")
                           };

            foreach (Token expectedToken in expected)
            {
                Token t = lexer.NextToken();
                Assert.That(t.Type, Is.EqualTo(expectedToken.Type));
                Assert.That(t.Value, Is.EqualTo(expectedToken.Value));
            }
        }

        [Test]
        public void ParserCanParse()
        {
            var lexer = new Lexer("1+3 - (6%2) / 14");
            var parser = new Parser(lexer);
            Assert.DoesNotThrow(() => parser.Parse());
        }

        [TestCase("1+3 - (6%2) / 14", 4)]
        [TestCase("-1", -1)]
        [TestCase("1+3 - (14%3) / 2", 3)]
        [TestCase("10 - (2+3)", 5)]
        [TestCase("10 - (2+3) * (14 % (7-2*2) * 3 -4)", 0)]
        public void EvaluationVisit(string input, int value)
        {
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);
            INode tree = parser.Parse();
            var visitor = new EvaluationVisitor();
            int result = visitor.Visit(tree);
            Assert.That(result, Is.EqualTo(value));
        }

        [TestCase("-1", "[- 1]")]
        [TestCase("1+3 - (6%2) / 14", "[- [+ 1 3] [/ [% 6 2] 14]]")]
        [TestCase("10 - (2+3)", "[- 10 [+ 2 3]]")]
        [TestCase("10 - (2+3) * (14 % (7-2*2) * 3 -4)", "[- 10 [* [+ 2 3] [- [* [% 14 [- 7 [* 2 2]]] 3] 4]]]")]
        [TestCase("1+2+3+4+5", "[+ [+ [+ [+ 1 2] 3] 4] 5]")]
        [TestCase("1*2+3*4+5*6+7", "[+ [+ [+ [* 1 2] [* 3 4]] [* 5 6]] 7]")]
        public void LispyVisit(string input, string lispy)
        {
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);
            INode tree = parser.Parse();
            var visitor = new LispyVisitor();
            string result = visitor.Visit(tree);
            Assert.That(result, Is.EqualTo(lispy));
        }
    }
}
