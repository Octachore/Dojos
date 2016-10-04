using System;

namespace Visitor.Exceptions
{
    [Serializable]
    internal class LexiconException : Exception
    {
        public LexiconException(string message) : base(message)
        {
        }
    }
}