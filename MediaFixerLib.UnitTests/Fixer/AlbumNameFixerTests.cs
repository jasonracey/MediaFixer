using FluentAssertions;
using MediaFixerLib.Fixer;
using NUnit.Framework;

namespace MediaFixerLib.UnitTests.Fixer
{
    [TestFixture]
    public class AlbumNameFixerTests
    {
        [TestCase("In Utero Disc1", "In Utero")]
        public void RemovesDiscAndNumberFromName(string before, string after)
        {
            before.FixAlbumName().Should().Be(after);
        }

        [TestCase("In Utero CD1", "In Utero")]
        public void RemovesCdAndNumberFromName(string before, string after)
        {
            before.FixAlbumName().Should().Be(after);
        }

        [Test]
        public void Trims()
        {
            "    Supernaut           "
                .FixAlbumName()
                .Should()
                .Be("Supernaut");
        }

        [Test]
        public void RemovesDoubleSpaces()
        {
            "My  Cool   Album"
                .FixAlbumName()
                .Should()
                .Be("My Cool Album");
        }

        [Test]
        public void ConvertsToTitleCase()
        {
            "tOwEr"
                .FixAlbumName()
                .Should()
                .Be("Tower");
        }

        [Test]
        public void FixesRomanNumerals()
        {
            "Symphony i, Symphony ii, Symphony iii"
                .FixAlbumName()
                .Should()
                .Be("Symphony I, Symphony II, Symphony III");
        }

        [Test]
        public void FixesRegionAbbreviations()
        {
            "2021-11-26 Issaquah Arena, Issaquah, wa"
                .FixAlbumName()
                .Should()
                .Be("2021-11-26 Issaquah Arena, Issaquah, WA");
        }
    }
}
