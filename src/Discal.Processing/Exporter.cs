using Discal.Model;
using Newtonsoft.Json;

namespace Discal.Processing
{
  public static class Exporter
  {
    public static void FullExport(StateManager state)
    {
      string file = @".\fullresult.txt";
      FileUtility.Delete(file);
      foreach(var comparison in state.Model.GeoComparisons)
      {
        FileUtility.WriteLine("******************************************************************************************", file);
        FileUtility.Write(comparison.GetGeoSummary(), file);
      }
    }

    public static void ExportText(StateManager state)
    {
      string file = @".\result.txt";
      FileUtility.Delete(file);
      foreach(var comparison in state.Model.GeoComparisons)
      {
        FileUtility.WriteLine(comparison.GetShortSummary(), file);
      }
    }

    public static void ExportJson(StateManager state)
    {
      string file = @".\result.json";
      FileUtility.Delete(file);
      string json = JsonConvert.SerializeObject(state.Model.GeoComparisons);
      FileUtility.WriteLine(json, file);
    }
  }
}
