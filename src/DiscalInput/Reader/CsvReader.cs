using System;
using System.Collections.Generic;
using Discal.Input.Reader;
using Discal.Model;

namespace Discal.Input
{
  public static class CsvReader
  {
    public static IEnumerable<Foundation> Read(string filePath)
    {
      int lineCounter = 0;
      string line;
      System.IO.StreamReader file = new System.IO.StreamReader(filePath);
      while((line = file.ReadLine()) != null)
      {
        lineCounter++;

        // Tries to parse individual line as a Foundation
        Foundation foundation = LineParser.TryParse(line);
        if(foundation != null)
        {
          // Cleans fields from extra unwanted characters
          DataCleaner.CleanFoundation(foundation);
          yield return foundation;
        }
        else
        {
          throw new Exception($"Error when attempting to read line #{lineCounter} from file: {filePath}");
        }
      }
    }
  }
}