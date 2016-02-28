using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegexLexcer
{
    public class CharacterSet : List<char>
    {
        public bool IsNegated { get; private set; }


        public CharacterSet(string characterSetToken, bool isNegated)
        {
            ParseToken(characterSetToken);
            IsNegated = isNegated;
        }

        Regex charSetMatcher = new Regex(@"(?<Range>\w-\w|\d-\d)|(?<Literal>[\d\w])|(<Special>\\\D)");
        private void ParseToken(string characterSetToken)
        {
            var matches = charSetMatcher.Matches(characterSetToken);
            foreach (Match match in matches)
            {
                var rangeGroup = match.Groups["Range"];
                var literalGroup = match.Groups["Literal"];
                var specialGroup = match.Groups["Special"];

                if (rangeGroup.Success) ParseRangeGroup(rangeGroup);
                if (literalGroup.Success) ParseLiteralGroup(literalGroup);
                if (specialGroup.Success) ParseSpecialGroup(specialGroup);
            }
        }

        private void ParseRangeGroup(Group rangeGroup)
        {
            var bounds = rangeGroup.Value.Split('-');
            if (bounds.Length != 2) throw new ArgumentOutOfRangeException("rangeGroup", rangeGroup.Value, "Ranged character token is inalid");
            try
            {
                throw new System.NotImplementedException();                
            }
            catch (Exception ex)
            {
                
            }
        }

        private void ParseLiteralGroup(Group literalGroup)
        {
            throw new System.NotImplementedException();
        }

        private void ParseSpecialGroup(Group specialGroup)
        {
            throw new System.NotImplementedException();
        }
    }
}