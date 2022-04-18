using System.Text.RegularExpressions;
using FluentAssertions;
using MediaFixerLib.Fixer;
using NUnit.Framework;

namespace MediaFixerLib.UnitTests.Fixer
{
    [TestFixture]
    public class TrackNameFixerTests
    {
        [TestCase(" alligator jam", "Alligator Jam")]
        [TestCase("alligator jam ", "Alligator Jam")]
        [TestCase(" alligator jam ", "Alligator Jam")]
        public void TrimsWhiteSpace(string before, string after)
        {
            TrackNameFixer.FixTrackName(before).Should().Be(after);
        }

        [Test]
        public void CollapsesInternalSpaces()
        {
            TrackNameFixer.FixTrackName("alligator      jam").Should().Be("Alligator Jam");
        }

        [Test]
        public void ConvertsToTitleCase()
        {
            TrackNameFixer.FixTrackName("aLlIgAtOr jAm").Should().Be("Alligator Jam");
        }

        [Test]
        public void FixesRomanNumerals()
        {
            TrackNameFixer.FixTrackName("iii. allegro").Should().Be("III. Allegro");
        }
    }

    [TestFixture]
    public class TrackNumberRegexTests
    {
        [TestCase("riot in cell block 9", "riot in cell block 9")]
        [TestCase("01riot in cell block 9", "riot in cell block 9")]
        [TestCase("01  riot in cell block 9", "riot in cell block 9")]
        [TestCase("01-riot in cell block 9", "riot in cell block 9")]
        [TestCase("01 - riot in cell block 9", "riot in cell block 9")]
        [TestCase("01 -- riot in cell block 9", "riot in cell block 9")]
        [TestCase("01.riot in cell block 9", "riot in cell block 9")]
        [TestCase("01 . riot in cell block 9", "riot in cell block 9")]
        [TestCase("01 .. riot in cell block 9", "riot in cell block 9")]
        public void ReturnsExpectedString(string before, string after)
        {
            TrackNameFixer.TrackNumberRegex
                .Replace(before, string.Empty)
                .Should()
                .Be(after);
        }
    }

    [TestFixture]
    public class VinylTrackNumberRegexTests
    {
        [TestCase("riot in cell block 9", "riot in cell block 9")]
        [TestCase("a-1-riot in cell block 9", "riot in cell block 9")]
        [TestCase("a - 1 - riot in cell block 9", "riot in cell block 9")]
        [TestCase("A-1-riot in cell block 9", "riot in cell block 9")]
        [TestCase("a-10-riot in cell block 9", "riot in cell block 9")]
        [TestCase("a - 10 - riot in cell block 9", "riot in cell block 9")]
        [TestCase("A-10-riot in cell block 9", "riot in cell block 9")]
        [TestCase("A - 10 - riot in cell block 9", "riot in cell block 9")]
        public void ReturnsExpectedString(string before, string after)
        {
            TrackNameFixer.VinylTrackNumberRegex
                .Replace(before, string.Empty)
                .Should()
                .Be(after);
        }
    }

    [TestFixture]
    public class GratefulDeadTrackNumberRegexTests
    {
        [TestCase("riot in cell block 9", "riot in cell block 9")]
        [TestCase("gdriot in cell block 9", "gdriot in cell block 9")]
        [TestCase("g riot in cell block 9", "g riot in cell block 9")]
        [TestCase("gd riot in cell block 9", "riot in cell block 9")]
        [TestCase("gd1 riot in cell block 9", "riot in cell block 9")]
        [TestCase("gd1  riot in cell block 9", "riot in cell block 9")]
        [TestCase("gd11 riot in cell block 9", "riot in cell block 9")]
        [TestCase("gd1t riot in cell block 9", "riot in cell block 9")]
        [TestCase("gd11t riot in cell block 9", "riot in cell block 9")]
        [TestCase("gd1t1 riot in cell block 9", "riot in cell block 9")]
        [TestCase("gd11t1 riot in cell block 9", "riot in cell block 9")]
        [TestCase("gd1t11 riot in cell block 9", "riot in cell block 9")]
        [TestCase("gd11t11 riot in cell block 9", "riot in cell block 9")]
        public void ReturnsExpectedString(string before, string after)
        {
            TrackNameFixer.GratefulDeadTrackNumberRegex
                .Replace(before, string.Empty)
                .Should()
                .Be(after);
        }
    }

    [TestFixture]
    public class LabeledTrackNumberRegexTests
    {
        private readonly Regex _trackLabelRegex = TrackNameFixer.GetTrackLabelRegex("track");

        [TestCase("riot in cell block 9", "riot in cell block 9")]
        [TestCase("tRaCk10riot in cell block 9", "riot in cell block 9")]
        [TestCase("tRaCk10-riot in cell block 9", "riot in cell block 9")]
        [TestCase("tRaCk10 riot in cell block 9", "riot in cell block 9")]
        [TestCase("tRaCk10 - riot in cell block 9", "riot in cell block 9")]
        [TestCase("tRaCk 10riot in cell block 9", "riot in cell block 9")]
        [TestCase("tRaCk 10-riot in cell block 9", "riot in cell block 9")]
        [TestCase("tRaCk 10 riot in cell block 9", "riot in cell block 9")]
        [TestCase("tRaCk 10 - riot in cell block 9", "riot in cell block 9")]
        public void ReturnsExpectedString(string before, string after)
        {
            _trackLabelRegex
                .Replace(before, string.Empty)
                .Should()
                .Be(after);
        }
    }
}
