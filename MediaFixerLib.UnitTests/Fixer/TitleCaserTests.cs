using FluentAssertions;
using MediaFixerLib.Fixer;
using NUnit.Framework;

namespace MediaFixerLib.UnitTests.Fixer
{
    [TestFixture]
    public class TitleCaserTests
    {
        [TestCase("hello world", "Hello World")]
        [TestCase("HELLO WORLD", "Hello World")]
        [TestCase("Hello World", "Hello World")]
        public void ReturnsExpectedString(string before, string after)
        {
            before
                .ToTitleCase()
                .Should()
                .Be(after);
        }
    }
}
