using System.IO;
using System.Linq;
using System.Text;
using Discal.Model;

namespace Discal.Console
{
  public static class StatisticsCalculator
  {
    private static string StatisticsFilePath = @".\statistics.txt";

    public static void GenerateStatisticsFile(StateManager state)
    {
      Clear();
      Save(GetStatistics(state));
    }

    public static string GetStatistics(StateManager state)
    {
      StringBuilder sb = new StringBuilder();

      int foundations = state.Model.Foundations.Length;
      int requestBatches = state.Model.RequestBatches.Count;
      int expectedComparisons = CalculateExpectedComparisons(state);
      int processedRequestBatches = state.Model.RequestBatches.Where(r => r.HasBeenProcessed).Count();
      int nonProcessedRequestBatches = state.Model.RequestBatches.Where(r => !r.HasBeenProcessed).Count();
      int totalGood = state.Model.RequestBatches.Where(r => r.Status.Equals("good")).Count();
      int totalDenied = state.Model.RequestBatches.Where(r => r.Status.Equals("denied")).Count();
      int totalFailed = state.Model.RequestBatches.Where(r => r.Status.Equals("failed")).Count();
      int totalOverQueryLimit = state.Model.RequestBatches.Where(r => r.Status.Equals("overquerylimit")).Count();
      int totalAttempted = state.Model.RequestBatches.Where(r => r.Status.Equals("attempted")).Count();
      int totalComparisons = state.Model.GeoComparisons.Count;

      decimal progress = totalComparisons * 100 / expectedComparisons;

      sb.AppendLine("***********************************************");
      sb.AppendLine($"PROGRESS {progress}%");
      sb.AppendLine("***********************************************");
      sb.AppendLine($"Foundations           : {foundations}");
      sb.AppendLine($"Request Batches       : {requestBatches}");
      sb.AppendLine($"Processed             : {processedRequestBatches}");
      sb.AppendLine($"Not Processed         : {nonProcessedRequestBatches}");
      sb.AppendLine($"Good                  : {totalGood}");
      sb.AppendLine($"Denied                : {totalDenied}");
      sb.AppendLine($"Failed                : {totalFailed}");
      sb.AppendLine($"Over Query Limit      : {totalOverQueryLimit}");
      sb.AppendLine($"Attempted             : {totalAttempted}");
      sb.AppendLine($"Expected Comparisons  : {expectedComparisons}");
      sb.AppendLine($"Created Comparisons   : {totalComparisons}");
      sb.AppendLine("***********************************************");
      return sb.ToString();
    }

    private static int CalculateExpectedComparisons(StateManager state)
    {
      int totalComparisons = 0;
      foreach(var requestBatch in state.Model.RequestBatches)
      {
        totalComparisons = totalComparisons + requestBatch.FoundationsToCompareAgainst.Count;
      }
      return totalComparisons;
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
