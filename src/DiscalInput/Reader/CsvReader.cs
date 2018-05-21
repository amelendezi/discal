using System.Collections.Generic;
using Discal.Input.Reader;
using Discal.Common;
using Discal.Model;

namespace Discal.Input
{
  public class CsvReader : IReader
  {
    public IEnumerable<Foundation> Read(string filePath, ILogger logger)
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
          logger.Log($"Error en lectura de la linea #{lineCounter} en el archivo: {filePath}");
        }
      }
    }
  }
}