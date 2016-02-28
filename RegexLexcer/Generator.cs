using System;
using System.Text.RegularExpressions;

namespace RegexLexcer
{
    public class Generator
    {
        Random rand = new Random(5);

        public string GetString(Regex stringRegexPattern)
        {
            stringRegexPattern.Match("YCF-185");
            return stringRegexPattern.ToString();
        }

        Regex tokenMatcher = new Regex(@"(?<CharSet>\[\^?[\w\d-]*\])((?:\{\d,?\d*\})|\*|\+|\?)?");
        public void RegexLexcer(string regexStringMask)
        {
            foreach (Match match in tokenMatcher.Matches(regexStringMask))
            {
                //match.Groups["CharSet"]
            }
        }
    }
}