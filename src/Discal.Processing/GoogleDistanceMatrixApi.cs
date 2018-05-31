using System;
using System.IO;
using System.Net;
using Discal.Model;
using Newtonsoft.Json;

namespace Discal.Processing
{
  public static class GoogleDistanceMatrixApi
  {
    public static GoogleCallResponse GetResponseFromGoogleApi(string url)
    {
      using(var client = new WebClient())
      {
        var uri = new Uri(url);

        var streamReader = new StreamReader(client.OpenRead(url));
        if(streamReader == null)
        {
          throw new Exception("Failed: stream reader is null");
        }
        else
        {
          return JsonConvert.DeserializeObject<GoogleCallResponse>(streamReader.ReadToEnd());
        }
      }
    }
  }
}
