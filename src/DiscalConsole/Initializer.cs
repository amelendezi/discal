using System;
using System.Collections.Generic;
using System.IO;
using Discal.Model;
using Newtonsoft.Json;

namespace Discal.Console
{
  public static class Initializer
  {
    public static void ProductionConfig()
    {
      var config = new Config()
      {
        ImportFilePath = @"C:\amedev\projects\discal\src\Resources\input.csv",
        ModelFilePath = @".\model.json",
        LogFilePath = @".\log.txt",
        GoogleApiKeys = BuildKeys()
      };

      Save(config);
    }

    public static void DevConfig()
    {
      var config = new Config()
      {
        ImportFilePath = @"C:\amedev\projects\discal\src\Resources\input-dev.csv",
        ModelFilePath = @".\model.json",
        LogFilePath = @".\log.txt",
        GoogleApiKeys = new List<ApiKey>() { BuildKey("AIzaSyC--chDrOa06MHTrdmkt5L-Wg26TiYHKLk") }
      };

      Save(config);
    }

    private static List<ApiKey> BuildKeys()
    {
      var keys = new List<ApiKey>();
      keys.Add(BuildKey("AIzaSyDSN-SLeQKlsmBYmYfSsWDujG-A6rrOnT0"));
      keys.Add(BuildKey("AIzaSyAJfJrOV2WorWVbaoC_ynTxHSBE-q1LmZY"));
      keys.Add(BuildKey("AIzaSyDf434nO_Ty2MDO9RxsLwBtkyxOQxuF0yc"));
      keys.Add(BuildKey("AIzaSyAzQywFqJZo4OX2aaf960IOa2-yKbPSg4U"));
      keys.Add(BuildKey("AIzaSyCsFXQpNDC1hWIfN3SZVc-lrG8fW1H4NTg"));
      keys.Add(BuildKey("AIzaSyA-F9yMfbTlcSdcdqymW3vJo22OhifscuI"));
      keys.Add(BuildKey("AIzaSyCk4S0-bazq0bUX-YCMH9gr9wxy0h3kh5U"));
      keys.Add(BuildKey("AIzaSyD00jj7nLLT7zrvlAfVbBasdANwecFGJrM"));
      keys.Add(BuildKey("AIzaSyDVi_09TsNGZEHdVA0aYbe6JmqAKZxGXGM"));
      keys.Add(BuildKey("AIzaSyB81rrIFoUgGtx4gFgaMU-hQhFdrfxPxg0"));
      keys.Add(BuildKey("AIzaSyBgNactEzlYd00B5rrTOocLhnNEIVITN9k"));
      keys.Add(BuildKey("AIzaSyARZRomXq2qVTw5hS2BrZq0dpgmLaq7x_g"));
      return keys;
    }

    private static ApiKey BuildKey(string key)
    {
      return new ApiKey()
      {
        Active = true,
        LastDateOfUse = DateTime.UtcNow,
        Value = key
      };
    }

    public static void Save(Config config)
    {
      var json = JsonConvert.SerializeObject(config);
      Write(@".\config.json", json);
    }

    private static void Write(string outputFilePath, string content)
    {
      Cleanup(outputFilePath);
      using(StreamWriter file = new StreamWriter(outputFilePath))
      {
        file.Write(content);
      }
    }

    private static void Cleanup(string outputFilePath)
    {
      if(File.Exists(outputFilePath))
      {
        File.Delete(outputFilePath);
      }
    }
  }
}
