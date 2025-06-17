using FluentAssertions;
using MediaFixerLib.Fixer;
using NUnit.Framework;

namespace MediaFixerLib.UnitTests.Fixer
{
    [TestFixture]
    public class GratefulDeadTrackNameFixerTests
    {
        [TestCase("Shakedown Street > (Live at Capital Centre, Landover, MD, 9/15/1982)", "Shakedown Street >")]
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
