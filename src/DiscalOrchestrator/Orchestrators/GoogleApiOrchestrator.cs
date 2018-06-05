using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
      StringBuilder log = new StringBuilder();

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
          log.AppendLine("------------------------------------------------------");
          log.AppendLine($"Executing request ...");
          // Execute api call          
          Thread.Sleep(5000);

          GoogleCallResponse response = GoogleDistanceMatrixApi.GetResponseFromGoogleApi(request.RequestUrl);

          if(response.Status == "OK")
          {
            // Handles a successfull response
            HandleSuccessFullResponse(state, request, response, log);
          }
          else
          {
            // Failed because of query limit or any other failure in the HTTP request,
            // Returns a new key if query limit was met, and breaks the execution of the
            // for loop to re-process the whole list (will skip already processed items)
            key = HandleFailedResponse(state, response, key, request, log);
            break;
          }
          Logger.Write(log.ToString(), state.Config.LogFilePath);
          Console.WriteLine(log.ToString());
          log.Clear();
          state.SaveModel();
        }

        log.AppendLine("Finished");
        Logger.Write(log.ToString(), state.Config.LogFilePath);
        Console.WriteLine("Finished");
      }
    }

    private static ApiKey HandleFailedResponse(StateManager state, GoogleCallResponse response, ApiKey key, RequestBatch request, StringBuilder log)
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
        log.AppendLine("The key has reached the limit: OVER_QUERY_LIMIT");
      }
      else if(response.Status == "REQUEST_DENIED")
      {
        request.HasBeenProcessed = true;
        request.Status = "denied";
        log.AppendLine($"Request Denied: {request.RequestUrl}");
      }
      else
      {
        request.HasBeenProcessed = true;
        request.Status = "failed";
        log.AppendLine($"Request Failed: {request.RequestUrl}");
        Console.ReadKey();
      }
      state.SaveModel();
      return key;
    }

    private static void HandleSuccessFullResponse(StateManager state, RequestBatch request, GoogleCallResponse response, StringBuilder log)
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
        log.AppendLine("Request has been successfull");
      }
      else
      {
        request.HasBeenProcessed = true;
        request.Status = "attempted";
        log.AppendLine($"Failed building Geo Comparison: {request.RequestUrl}");
        Console.ReadKey();
      }
    }
  }
}
