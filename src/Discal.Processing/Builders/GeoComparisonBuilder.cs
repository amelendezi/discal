using System;
using System.Collections.Generic;
using Discal.Model;
using static Discal.Model.GoogleCallResponse;

namespace Discal.Processing.Builders
{
  public static class GeoComparisonBuilder
  {
    public static List<GeoComparison> BuildGeoComparisons(RequestBatch request, GoogleCallResponse response)
    {
      // Precondition: response should have exactly 1 row
      Row[] rows = response.Rows;
      if(rows.Length > 1 || rows.Length < 1)
      {
        Console.WriteLine($"GeoComparisonBuilder FAILED: Returned multiple rows at request {request.RequestUrl}");
        return null;
      }

      Row row = rows[0];
      Element[] elements = row.Elements;

      // Precondition: response should have same amount of elements as the foundations to be compaired against
      if(elements.Length != request.FoundationsToCompareAgainst.Count)
      {
        Console.WriteLine($"GeoComparisonBuilder FAILED [{DateTime.Now}] : Inconsistent number of elements in relation to foundations compared at request {request.RequestUrl}");
        return null;
      }

      // Map the response to the GeoComparison
      var geoComparisons = new List<GeoComparison>();

      Foundation source = request.Foundation;
      for(int i = 0; i < elements.Length; i++)
      {
        Foundation target = request.FoundationsToCompareAgainst[i];
        Element element = elements[i];

        var geoComparison = new GeoComparison()
        {
          Source = source,
          Target = target,
          Geo = GetGeo(element)
        };

        geoComparisons.Add(geoComparison);
      }
      return geoComparisons;
    }

    private static GeoData GetGeo(Element element)
    {
      return new GeoData()
      {
        DistanceText = element.Distance.Text,
        Distance = element.Distance.Value,
        DurationText = element.Duration.Text,
        Duration = element.Duration.Value
      };
    }
  }
}
