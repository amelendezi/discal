using System;
using System.Collections.Generic;
using System.Linq;
using Discal.Model;
using Discal.Processing;
using Discal.Processing.Builders;

namespace Discal.Orchestration.Orchestrators
{
  public static class GoogleApiOrchestrator
  {
    public static void Run(StateManager state)
    {
      ApiKey key = state.Config.GoogleApiKeys.FirstOrDefault(k => k.Active);
      if(key != null)
      {
        foreach(RequestBatch request in state.Model.RequestBatches)
        {
          if(request.HasBeenProcessed)
          {
            continue;
          }

          // Build the URL
          ApiCallUrlBuilder.BuildRequestBatchUrl(request, key.Value);

          // Execute api call
          GoogleCallResponse response = GoogleDistanceMatrixApi.GetResponseFromGoogleApi(request.RequestUrl);

          if(response.Status == "OK")
          {
            // Build GeoComparison and append to collection
            List<GeoComparison> geoComparisons = GeoComparisonBuilder.BuildGeoComparisons(request, response);
            if(geoComparisons != null)
            {
              // Add geoComparisons to collection
              if(state.Model.GeoComparisons == null)
              {
                state.Model.GeoComparisons = new List<GeoComparison>();
              }
              state.Model.GeoComparisons.AddRange(geoComparisons);
              request.HasBeenProcessed = true;
              request.Status = "good";
            }
            else
            {
              request.HasBeenProcessed = false;
              request.Status = "attempted";
              Console.WriteLine("Something failed when create the GeoComparison");
              Console.ReadKey();
            }
          }
          else
          {
            if(response.Status == "OVER_QUERY_LIMIT")
            {
              // Disable the key
              key.Active = false;
              key = state.Config.GoogleApiKeys.FirstOrDefault(k => k.Active);
              request.HasBeenProcessed = false;
              request.Status = "overquerylimit";
              state.SaveConfig();

              // There are no more keys available, so we exit the application
              if(key == null)
              {
                state.SaveModel();
                Console.WriteLine("You are out of valid keys for today");
                Console.ReadKey();
                Environment.Exit(0);
              }
            }
            else
            {
              request.HasBeenProcessed = true;
              request.Status = "failed";
              Console.WriteLine($"A request has failed: {request.RequestUrl}");
              Console.ReadKey();
            }
          }
          state.SaveModel();
        }
      }
    }
  }
}
