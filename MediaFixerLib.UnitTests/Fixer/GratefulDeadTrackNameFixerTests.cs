using FluentAssertions;
using MediaFixerLib.Fixer;
using NUnit.Framework;

namespace MediaFixerLib.UnitTests.Fixer
{
    [TestFixture]
    public class GratefulDeadTrackNameFixerTests
    {
        [TestCase("Alligator>", "Alligator >")]
        [TestCase("Alligator >", "Alligator >")]
        [TestCase("Alligator  >", "Alligator >")]
        [TestCase("Alligator->", "Alligator >")]
        [TestCase("Alligator ->", "Alligator >")]
        [TestCase("Alligator - >", "Alligator >")]
        [TestCase("Alligator Jam", "Alligator Jam")]
        [TestCase("Alligator Jam>", "Alligator Jam >")]
        [TestCase("Alligator Jam >", "Alligator Jam >")]
        [TestCase("Alligator Jam->", "Alligator Jam >")]
        [TestCase("Alligator Jam ->", "Alligator Jam >")]
        [TestCase("Alligator Jam - >", "Alligator Jam >")]
        [TestCase("other one jam > cryptical jam >", "The Other One Jam > Cryptical Envelopment Jam >")]
        public void FixTrackName(string before, string after)
        {
            GratefulDeadTrackNameFixer.FixTrackName(before).Should().Be(after);
        }
    }
}
