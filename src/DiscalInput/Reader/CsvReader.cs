using System;
using System.Collections.Generic;
using Discal.Input.DomainModel;

namespace Discal.Input.Reader
{
  public class CsvReader : IReader
  {
    public IEnumerable<Foundation> Read(string filePath)
    {
      string line;
      System.IO.StreamReader file = new System.IO.StreamReader(filePath);
      while((line = file.ReadLine()) != null)
      {
        yield return Parse(line);
      }
    }

    private Foundation Parse(string line)
    {
      string[] segments = line.Split(';');
      var foundation = new Foundation()
      {
        Number = Int32.Parse(segments[0]),
        Nit = segments[1],
        Description = segments[2],
        Neighborhood = segments[3],
        Location = segments[4],
        Address = segments[5],
        Coordinates = segments[6]
      };
      return foundation;
    }

  }
}
