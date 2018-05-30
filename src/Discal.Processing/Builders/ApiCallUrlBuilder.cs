using System.Collections.Generic;
using System.Text;
using Discal.Model;

namespace Discal.Processing.Builders
{
  public static class ApiCallUrlBuilder
  {
    public static void BuildRequestBatchUrl(RequestBatch requestBatch, string key)
    {
      string baseUrl = @"https://maps.googleapis.com/maps/api/distancematrix/json?";
      string origins = $"origins={requestBatch.Foundation.Coordinates}";
      string destinations = ComposeParams("destinations", requestBatch.FoundationsToCompareAgainst);
      requestBatch.RequestUrl = $"{baseUrl}{origins}&{destinations}&key={key}";
    }

    private static string ComposeParams(string prefix, List<Foundation> collection)
    {
      StringBuilder builder = new StringBuilder();
      builder.Append(prefix);
      builder.Append("=");

      for(int i = 0; i < collection.Count; i++)
      {
        builder.Append(collection[i].Coordinates);
        if(i < collection.Count - 1)
        {
          builder.Append("|");
        }
      }
      return builder.ToString();
    }
  }
}
