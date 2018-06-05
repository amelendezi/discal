using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
      while(key != null && state.Model.RequestBatches.Any(r => !r.HasBeenProcessed))
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
          Thread.Sleep(5000);
          GoogleCallResponse response = GoogleDistanceMatrixApi.GetResponseFromGoogleApi(request.RequestUrl);

          if(response.Status == "OK")
          {
            // Handles a successfull response
            HandleSuccessFullResponse(state, request, response);
          }
          else
          {
            // Failed because of query limit or any other failure in the HTTP request,
            // Returns a new key if query limit was met, and breaks the execution of the
            // for loop to re-process the whole list (will skip already processed items)
            key = HandleFailedResponse(state, response, key, request);
            break;
          }
          state.SaveModel();
        }
      }
    }

    private static ApiKey HandleFailedResponse(StateManager state, GoogleCallResponse response, ApiKey key, RequestBatch request)
    {
      if(response.Status == "OVER_QUERY_LIMIT")
      {
        // Disable the key
        key.Active = false;

        // Get a new key (or null)
        key = state.Config.GoogleApiKeys.FirstOrDefault(k => k.Active);
        request.HasBeenProcessed = false;
        request.Status = "overquerylimit";
        state.SaveConfig();
        Console.WriteLine("Over query limit");
      }
      else if(response.Status == "REQUEST_DENIED")
      {
        request.HasBeenProcessed = true;
        request.Status = "denied";
        Console.WriteLine("Request has been denied");
      }
      else
      {
        request.HasBeenProcessed = true;
        request.Status = "failed";
        Console.WriteLine($"A request has failed");
      }
      state.SaveModel();
      return key;
    }

    private static void HandleSuccessFullResponse(StateManager state, RequestBatch request, GoogleCallResponse response)
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
        Console.WriteLine("Request handled successfully");
      }
      else
      {
        request.HasBeenProcessed = true;
        request.Status = "attempted";
        Console.WriteLine("Something failed when create the GeoComparison");
        Console.ReadKey();
      }
    }
  }
}
