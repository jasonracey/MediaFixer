using FluentAssertions;
using MediaFixerLib.Fixer;
using NUnit.Framework;

namespace MediaFixerLib.UnitTests.Fixer
{
    [TestFixture]
    public class StringExtensionTests
    {
        [TestCase("", "")]
        [TestCase("  ", " ")]
        [TestCase("   ", " ")]
        [TestCase("alligator  jam", "alligator jam")]
        [TestCase("alligator  jam  > strawberry   jam", "alligator jam > strawberry jam")]
        public void RemoveDoubleSpaces(string before, string after)
        {
            before.RemoveDoubleSpaces().Should().Be(after);
        }

        [TestCase("alligator    jam", "  ", " ", "alligator jam")]
        [TestCase("alligator  jam", "  ", ".", "alligator.jam")]
        public void RepeatedlyReplace(string before, string match, string substitution, string after)
        {
            before.RepeatedlyReplace(match, substitution).Should().Be(after);
        }

        [TestCase("", "")]
        [TestCase(".", "")]
        [TestCase("a", "a")]
        [TestCase("ab", "ab")]
        [TestCase("!a@b#", "ab")]
        [TestCase("1", "1")]
        [TestCase("12", "12")]
        [TestCase("!1@2#", "12")]
        [TestCase("1a", "1a")]
        [TestCase("1a2b", "1a2b")]
        [TestCase("!1a@2b#", "1a2b")]
        [TestCase("A", "a")]
        [TestCase("AB", "ab")]
        [TestCase("!A@B#", "ab")]
        [TestCase("1A", "1a")]
        [TestCase("1A2B", "1a2b")]
        [TestCase("!1A@2B#", "1a2b")]
        public void ToLowerAlphaNumeric(string before, string after)
        {
            before.ToLowerAlphaNumeric().Should().Be(after);
        }
    }
}