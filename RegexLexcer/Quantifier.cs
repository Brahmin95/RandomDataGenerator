using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegexLexcer
{
    public class Quantifier
    {
        public int Lower { get; private set; }
        public int Upper { get; private set; }
        public Types Type { get; private set; }

        public Quantifier(string quantifierToken)
        {
            ParseToken(quantifierToken);
        }

        //todo possbily make this an extension method as this is used for Random data generation (seperate to regex parsing)
        public int GetLength(Random rand)
        {
            return rand.Next(Lower, Upper);
        }

        // todo possibility to reove this subtype if not actually used
        public enum Types
        {
            Fixed,
            Range
        }

        private static Regex repetitionMatcher = new Regex(@"\{(?<numeric>\d+,?\d*)\}|(?<special>[\*\+\?])");
        private void ParseToken(string token)
	    {
		    var matches = repetitionMatcher.Matches(token);
		    if (matches.Count > 1) throw new ArgumentException("Too many quantifier matches", "token");
		    if (matches.Count == 0) throw new ArgumentException("Too few quantifier matches", "token");

		    var match = matches[0];
		    var numericGroup = match.Groups["numeric"];
		    var specialGroup = match.Groups["special"];
            if (specialGroup.Success && numericGroup.Success)
                throw new ArgumentException("Quantifier cannot use a combination of numeric and special tokens", "token");
		    
            if (numericGroup.Success) ParseNumericQuantifier(numericGroup);
            if (specialGroup.Success) ParseSpecialQuantifier(specialGroup);
	    }

        private void ParseNumericQuantifier(Group numericGroup)
        {
            var bounds = numericGroup.Value.Split(',');
            try
            {
                switch (bounds.Length)
                {
                    case 1:
                        Lower = Upper = int.Parse(bounds[0]);
                        break;
                    case 2:
                        Lower = int.Parse(bounds[0]);
                        Upper = string.IsNullOrWhiteSpace(bounds[1]) ? int.MaxValue : int.Parse(bounds[1]);
                        break;
                    default:
                        throw new ArgumentException("Invalid number of bounds parameters in numeric quantifier token", "numericGroup"); 
                }
                if (Lower > Upper) 
                    throw new ArgumentOutOfRangeException("numericGroup", "The upper bounds of the numeric quantifier are less than the lower bounds");
                Type = Upper == Lower ? Types.Fixed : Types.Range;
            }
            catch (Exception ex)
            {
                throw new InvalidCastException("One of the numeric bounds parameters could not be cast to an integer.", ex);
            }
        }
        
        private void ParseSpecialQuantifier(Group specialGroup)
        {
            switch (specialGroup.Value)
            {
                case "*":
                    Type = Types.Range;
                    Lower = 0;
                    Upper = int.MaxValue;
                    break;
                case "+":
                    Type = Types.Range;
                    Lower = 1;
                    Upper = int.MaxValue;
                    break;
                case "?":
                    Type = Types.Range;
                    Lower = 0;
                    Upper = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("specialGroup", specialGroup.Value, "Unexpected Quantifier Token");
            }
        }
    }
}
