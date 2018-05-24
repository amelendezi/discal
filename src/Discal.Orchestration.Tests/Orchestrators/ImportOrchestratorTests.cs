using Discal.Console.State;
using Discal.Orchestration.Orchestrators;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Discal.Orchestration.Tests.Orchestrators
{
  [TestFixture]
  public class ImportOrchestratorTests
  {
    [Test]
    public void Import300Foundations_BuildsRequestBatches_AndValidatesTotalComparisons()
    {
      StateManager state = new StateManager();
      var orchestrator = new ImportOrchestrator();
      orchestrator.Run(state.Config, state.Model);

      int comparisonCount = 0;

      foreach(var requestBatch in state.Model.RequestBatches)
      {
        comparisonCount = comparisonCount + requestBatch.FoundationsToCompareAgainst.Count;
      }

      // Imports 300 foundations, which is a matrix of 90000 comparisons (300x300). We subtract the diagonal
      // which leaves 89700, and only calculate half of the matrix because the other half is the same. Leaving
      // us with 44850. (90000 - 300) / 2 = 44850
      Assert.AreEqual(expected: 44850, actual: comparisonCount);
      Assert.AreEqual(expected: 300, actual: state.Model.Foundations.Length);
    }
  }
}
