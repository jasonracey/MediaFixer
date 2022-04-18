using FluentAssertions;
using MediaFixerLib.Fixer;
using NUnit.Framework;

namespace MediaFixerLib.UnitTests.Fixer
{
    [TestFixture]
    public class RegionAbbreviationFixerTests
    {
        [TestCase("0000-00-00 alabama arena, alabama al", "0000-00-00 alabama arena, alabama al")]
        [TestCase("0000-00-00 alabama arena, alabama al hambra", "0000-00-00 alabama arena, alabama al hambra")]
        [TestCase("0000-00-00 alabama arena, alabama, al hambra", "0000-00-00 alabama arena, alabama, al hambra")]
        [TestCase("0000-00-00 alabama arena, alabama alhambra", "0000-00-00 alabama arena, alabama alhambra")]
        [TestCase("0000-00-00 alabama arena, alabama, alhambra", "0000-00-00 alabama arena, alabama, alhambra")]
        [TestCase("0000-00-00 alabama arena, alabama,al", "0000-00-00 alabama arena, alabama,AL")]
        [TestCase("0000-00-00 alabama arena, alabama,al ", "0000-00-00 alabama arena, alabama,AL")]
        [TestCase("0000-00-00 alabama arena, alabama, al", "0000-00-00 alabama arena, alabama, AL")]
        [TestCase("0000-00-00 alabama arena, alabama, al ", "0000-00-00 alabama arena, alabama, AL")]
        [TestCase("0000-00-00 alabama arena, alabama , al", "0000-00-00 alabama arena, alabama , AL")]
        [TestCase("0000-00-00 alabama arena, alabama , al ", "0000-00-00 alabama arena, alabama , AL")]
        public void ReturnsExpectedString(string before, string after)
        {
            before
                .FixRegionAbbreviation()
                .Should()
                .Be(after);
        }
    }
}
