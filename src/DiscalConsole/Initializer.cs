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
        ImportFilePath = @".\input.csv",
        ModelFilePath = @".\model.json",
        LogFilePath = @".\log.txt",
        GoogleApiKeys = BuildKeys()
      };

      Save(config);
    }

    private static List<ApiKey> BuildKeys()
    {
      using(StreamReader reader = new StreamReader(@"C:\keys.txt"))
      {
        string rawKeys = reader.ReadToEnd();
        string[] keyLiterals = rawKeys.Split(',');

        var keys = new List<ApiKey>();

        foreach(string literal in keyLiterals)
        {
          keys.Add(BuildKey(literal));
        }
        return keys;
      }
    }

    private static ApiKey BuildKey(string key)
    {
      return new ApiKey()
      {
        Active = true,
        LastDateOfUse = DateTime.Today,
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
