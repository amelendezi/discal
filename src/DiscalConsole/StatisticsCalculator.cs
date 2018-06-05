using System.IO;
using System.Linq;
using System.Text;
using Discal.Model;

namespace Discal.Console
{
  public static class StatisticsCalculator
  {
    private static string StatisticsFilePath = @".\statistics.txt";

    public static void Statistics(StateManager state)
    {
      Clear();

      Save($"Foundations       : {state.Model.Foundations.Length}");
      Save($"Request Batches   : {state.Model.RequestBatches.Count}");

      int totalComparisons = 0;
      foreach(var requestBatch in state.Model.RequestBatches)
      {
        totalComparisons = totalComparisons + requestBatch.FoundationsToCompareAgainst.Count;
      }
      Save($"Total Comparisons : {totalComparisons}");

      int totalProcessed = state.Model.RequestBatches.Where(r => r.HasBeenProcessed).Count();
      Save($"Processed         : {totalProcessed}");

      int totalNotProcessed = state.Model.RequestBatches.Where(r => !r.HasBeenProcessed).Count();
      Save($"Not Processed     : {totalNotProcessed}");

      int totalGood = state.Model.RequestBatches.Where(r => r.Status.Equals("good")).Count();
      int totalDenied = state.Model.RequestBatches.Where(r => r.Status.Equals("denied")).Count();
      int totalFailed = state.Model.RequestBatches.Where(r => r.Status.Equals("failed")).Count();
      int totalOverQueryLimit = state.Model.RequestBatches.Where(r => r.Status.Equals("overquerylimit")).Count();
      int totalAttempted = state.Model.RequestBatches.Where(r => r.Status.Equals("attempted")).Count();

      Save($"Total Good        : {totalGood}");
      Save($"Total Denied      : {totalDenied}");
      Save($"Total Failed      : {totalFailed}");
      Save($"Total OverQueryL  : {totalOverQueryLimit}");
      Save($"Total Attempted   : {totalAttempted}");
    }

    private static void Clear()
    {
      if(File.Exists(StatisticsFilePath))
      {
        File.Delete(StatisticsFilePath);
      }
    }

    private static void Save(string message)
    {
      if(!File.Exists(StatisticsFilePath))
      {
        using(StreamWriter file = new StreamWriter(StatisticsFilePath))
        {
          file.WriteLine(message);
          file.Close();
        }
      }
      else
      {
        using(StreamWriter file = File.AppendText(StatisticsFilePath))
        {
          file.WriteLine(message);
          file.Close();
        }
      }
    }
  }
}
