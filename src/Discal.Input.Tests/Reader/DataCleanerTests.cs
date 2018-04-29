using Discal.Input.Reader;
using NUnit.Framework;

namespace Discal.Input.Tests.Reader
{
  [TestFixture]
  public class DataCleanerTests
  {
    [Test]
    public void Foundation_WithFieldsWithExtraSpaces_AreCleaned()
    {
      // Prepare
      var foundation = new Foundation()
      {
        Number = 1,
        Nit = "  8874378  ",
        Description = "Some    description with spaces     ",
        Neighborhood = "   A    Neighborhood      ",
        Location = "   A Location     ",
        Address = "Address       ",
        Coordinates = "-71.04343,      75.8954784          "
      };

      // Act
      DataCleaner.CleanFoundation(foundation);

      // Assert
      Assert.AreEqual(expected: "8874378", actual: foundation.Nit);
      Assert.AreEqual(expected: "Some description with spaces", actual: foundation.Description);
      Assert.AreEqual(expected: "A Neighborhood", actual: foundation.Neighborhood);
      Assert.AreEqual(expected: "A Location", actual: foundation.Location);
      Assert.AreEqual(expected: "Address", actual: foundation.Address);
      Assert.AreEqual(expected: "-71.04343, 75.8954784", actual: foundation.Coordinates);
    }
  }
}
