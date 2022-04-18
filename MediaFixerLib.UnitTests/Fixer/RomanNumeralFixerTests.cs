using FluentAssertions;
using MediaFixerLib.Fixer;
using NUnit.Framework;

namespace MediaFixerLib.UnitTests.Fixer
{
    [TestFixture]
    public class RomanNumeralFixerTests
    {
        [TestCase("i ii iii iv v vi vii viii ix x xx xxx xl l lx lxx lxxx xc c", "I II III IV V VI VII VIII IX X XX XXX XL L LX LXX LXXX XC C")]
        [TestCase("i.", "I.")]
        [TestCase("iallegro", "iallegro")]
        [TestCase("i allegro", "I allegro")]
        [TestCase("i.allegro", "I.allegro")]
        [TestCase("i. allegro", "I. allegro")]
        public void ReturnsExpectedString(string before, string after)
        {
            before
                .FixRomanNumerals()
                .Should()
                .Be(after);
        }
    }
}
