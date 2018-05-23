using System.Collections.Generic;
using Discal.Model;
using Discal.Processing.Builders;
using NUnit.Framework;

namespace Discal.Processing.Tests.Builders
{
  [TestFixture]
  public class RequestBatchBuilderTests
  {
    [Test]
    public void BuildRequestBatches_WithSpecificBufferSize_BuildsCorrectNumberOfBatches()
    {
      List<Foundation> foundations = new List<Foundation>();
      foundations.Add(new Foundation() { Nit = "Nit0", Coordinates = "0,0", Number = 0 });
      foundations.Add(new Foundation() { Nit = "Nit1", Coordinates = "0,0", Number = 1 });
      foundations.Add(new Foundation() { Nit = "Nit2", Coordinates = "0,0", Number = 2 });
      foundations.Add(new Foundation() { Nit = "Nit3", Coordinates = "0,0", Number = 3 });
      foundations.Add(new Foundation() { Nit = "Nit4", Coordinates = "0,0", Number = 4 });

      var batches = RequestBatchBuilder.BuildRequestBatches(foundations.ToArray(), 2);

      Assert.AreEqual(expected: 6, actual: batches.Count);
      Assert.AreEqual(expected: 2, actual: batches[0].FoundationsToCompareAgainst.Count);
      Assert.AreEqual(expected: 2, actual: batches[1].FoundationsToCompareAgainst.Count);
      Assert.AreEqual(expected: 2, actual: batches[2].FoundationsToCompareAgainst.Count);
      Assert.AreEqual(expected: 1, actual: batches[3].FoundationsToCompareAgainst.Count);
      Assert.AreEqual(expected: 2, actual: batches[4].FoundationsToCompareAgainst.Count);
      Assert.AreEqual(expected: 1, actual: batches[5].FoundationsToCompareAgainst.Count);
    }
  }
}
