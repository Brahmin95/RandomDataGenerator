using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace RegexLexcer.UnitTests
{
    [TestClass]
    public class Suite0010_QuantifierTests
    {
        [TestMethod]
        public void Test0010_ConstuctorAcceptsStarQuantifier()
        {
            Quantifier sut = new Quantifier("*");
            sut.Should().NotBeNull("the constructor should accept this input");
            sut.Lower.Should().Be(0, "the lower value of '*' is 0");
            sut.Upper.Should()
                .Be(int.MaxValue, "the infinite upper vlue of '*' has been capped at the maximum value of int.");
            sut.Type.Should().Be(Quantifier.Types.Range);
        }

        [TestMethod]
        public void Test0020_ConstuctorAcceptsPlusQuantifier()
        {
            Quantifier sut = new Quantifier("+");
            sut.Should().NotBeNull("the constructor should accept this input");
            sut.Lower.Should().Be(1, "the lower value of '+' is 1");
            sut.Upper.Should()
                .Be(int.MaxValue, "the infinite upper vlue of '+' has been capped at the maximum value of int.");
            sut.Type.Should().Be(Quantifier.Types.Range);
        }

        [TestMethod]
        public void Test0030_ConstuctorAcceptsQuestionMarkQuantifier()
        {
            Quantifier sut = new Quantifier("?");
            sut.Should().NotBeNull("the constructor should accept this input");
            sut.Lower.Should().Be(0, "the lower value of '?' is 0");
            sut.Upper.Should().Be(1, "the upper value of '?' is 1.");
            sut.Type.Should().Be(Quantifier.Types.Range);
        }

        [TestMethod]
        public void Test0040_ConstuctorAcceptsFixedNumericQuantifier()
        {
            var fixedNum = 7;
            var testValue = string.Format("{{{0}}}", fixedNum);
            Quantifier sut = new Quantifier(testValue);
            sut.Should().NotBeNull("the constructor should accept this input");
            sut.Lower.Should().Be(7, "the lower value of numeric quantifier '{0}' is {1}.", testValue, fixedNum);
            sut.Upper.Should().Be(7, "the upper value of numeric quantifier '{0}' is {1}.", testValue, fixedNum);
            sut.Type.Should().Be(Quantifier.Types.Fixed);
        }

        [TestMethod]
        public void Test0050_ConstuctorAcceptsInfinateNumericQuantifier()
        {
            var fixedNum = 7;
            var testValue = string.Format("{{{0},}}", fixedNum);
            Quantifier sut = new Quantifier(testValue);
            sut.Should().NotBeNull("the constructor should accept this input");
            sut.Lower.Should().Be(7, "the lower value of numeric quantifier '{0}' is {1}.", testValue, fixedNum);
            sut.Upper.Should().Be(int.MaxValue, "the upper value of numeric quantifier '{0}' is {1}.", testValue, int.MaxValue);
            sut.Type.Should().Be(Quantifier.Types.Range);
        }

        [TestMethod]
        public void Test0060_ConstuctorAcceptsBoundedNumericQuantifier()
        {
            var lowerNum = 7;
            var upperNum = 95;
            var testValue = string.Format("{{{0},{1}}}", lowerNum, upperNum);
            Quantifier sut = new Quantifier(testValue);
            sut.Should().NotBeNull("the constructor should accept this input");
            sut.Lower.Should().Be(7, "the lower value of numeric quantifier '{0}' is {1}.", testValue, lowerNum);
            sut.Upper.Should().Be(95, "the upper value of numeric quantifier '{0}' is {1}.", testValue, upperNum);
            sut.Type.Should().Be(Quantifier.Types.Range);
        }

        [DataTestMethod]
        [DataRow("{1}", 1, 1)]
        [DataRow("{9}", 9, 9)]
        [DataRow("{473}", 473, 473)]
        [DataRow("{1,}", 1, int.MaxValue)]
        [DataRow("{9,}", 9, int.MaxValue)]
        [DataRow("{34,}", 34, int.MaxValue)]
        [DataRow("{1245,}", 1245, int.MaxValue)]
        [DataRow("{1,3}", 1, 3)]
        [DataRow("{13,49}", 13, 49)]
        [DataRow("{2609,99999}", 2609, 99999)]
        public void Test0070_ConstuctorAcceptsAllValidNumericQuantifiers(string input, int lower, int upper)
        {
            Quantifier sut = new Quantifier(input);
            sut.Should().NotBeNull("the constructor should accept this input");
            sut.Lower.Should().Be(lower, "the lower value of numeric quantifier '{0}' is {1}.", input, lower);
            sut.Upper.Should().Be(upper, "the upper value of numeric quantifier '{0}' is {1}.", input, upper);
            //sut.Type.Should().Be(Quantifier.Types.Range);
        }
    }
}
