namespace RegexLexcer
{
    public class Token
    {
        public CharacterSet Candidates { get; private set; }
        public Quantifier Quantifier { get; private set; }


    }
}