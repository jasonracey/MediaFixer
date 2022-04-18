using FluentAssertions;
using MediaFixerLib.Fixer;
using NUnit.Framework;

namespace MediaFixerLib.UnitTests.Fixer
{
  [TestFixture]
  public class DiscNumberRemoverTests
  {
    [TestCase("In Utero1 disc1", "In Utero1")]
    [TestCase("In Utero1 disc01", "In Utero1")]
    [TestCase("In Utero1 disc 1", "In Utero1")]
    [TestCase("In Utero1 disc 01", "In Utero1")]
    [TestCase("In Utero1 [disc1]", "In Utero1")]
    [TestCase("In Utero1 [disc01]", "In Utero1")]
    [TestCase("In Utero1 [disc 1]", "In Utero1")]
    [TestCase("In Utero1 [disc 01]", "In Utero1")]
    [TestCase("In Utero1 (disc1)", "In Utero1")]
    [TestCase("In Utero1 (disc01)", "In Utero1")]
    [TestCase("In Utero1 (disc 1)", "In Utero1")]
    [TestCase("In Utero1 (disc 01)", "In Utero1")]
    public void RemovesDiscAndNumberFromName(string before, string after)
    {
        before
            .RemoveDiscNumber()
            .Should()
            .Be(after);
    }

    [TestCase("In Utero1 cd1", "In Utero1")]
    [TestCase("In Utero1 cd01", "In Utero1")]
    [TestCase("In Utero1 cd 1", "In Utero1")]
    [TestCase("In Utero1 cd 01", "In Utero1")]
    [TestCase("In Utero1 [cd1]", "In Utero1")]
    [TestCase("In Utero1 [cd01]", "In Utero1")]
    [TestCase("In Utero1 [cd 1]", "In Utero1")]
    [TestCase("In Utero1 [cd 01]", "In Utero1")]
    [TestCase("In Utero1 (cd1)", "In Utero1")]
    [TestCase("In Utero1 (cd01)", "In Utero1")]
    [TestCase("In Utero1 (cd 1)", "In Utero1")]
    [TestCase("In Utero1 (cd 01)", "In Utero1")]
    public void RemovesCdAndNumberFromName(string before, string after)
    {
        before
            .RemoveDiscNumber()
            .Should()
            .Be(after);
    }
  }
}
