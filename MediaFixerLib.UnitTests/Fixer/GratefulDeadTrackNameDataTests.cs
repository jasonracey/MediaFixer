using FluentAssertions;
using MediaFixerLib.Fixer;
using NUnit.Framework;

namespace MediaFixerLib.UnitTests.Fixer
{
    [TestFixture]
    public class GratefulDeadTrackNameDataTests
    {
        [TestCase("", null)]
        [TestCase(" ", null)]
        [TestCase("blah", null)]
        [TestCase("alligator", "Alligator")]
        [TestCase(" penelope  alligator armadillo  ", "Alligator")]
        public void Test(string term, string expected)
        {
            GratefulDeadTrackNameData.Search(term).Should().Be(expected);
        }
    }
}
