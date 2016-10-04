namespace Visitor.Parsing
{
    public enum TokenType
    {
        /// <summary>
        /// A number
        /// </summary>
        Number,

        /// <summary>
        /// Addition opperation
        /// </summary>
        AddOpp,

        /// <summary>
        /// Substraction opperation
        /// </summary>
        SubOpp,

        /// <summary>
        /// Multiplication opperation
        /// </summary>
        MultOpp,

        /// <summary>
        /// Division opperation
        /// </summary>
        DivOpp,

        /// <summary>
        /// Modulus opperation
        /// </summary>
        ModOpp,

        /// <summary>
        /// Opening perenthesis
        /// </summary>
        OpenPar,

        /// <summary>
        /// Closing parenthisis
        /// </summary>
        ClosePar,

        /// <summary>
        /// End of input
        /// </summary>
        EOI
    }
}