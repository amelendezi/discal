using System;
using System.Globalization;

namespace Discal.Input.Reader
{
  public static class LineParser
  {
    public static Foundation TryParse(string line)
    {
      Foundation foundation = null;
      string[] segments = line.Split(';');

      if(LineHasValidStructure(segments))
      {
        // Try to parse the Id as an Integer
        if(!Int32.TryParse(segments[0], out int id))
        {
          return null;
        }

        if(!HasValidCoordinates(segments[6]))
        {
          return null;
        }

        foundation = new Foundation()
        {
          Number = id,
          Nit = segments[1],
          Description = segments[2],
          Neighborhood = segments[3],
          Location = segments[4],
          Address = segments[5],
          Coordinates = segments[6]
        };
      }
      return foundation;
    }

    private static bool LineHasValidStructure(string[] segments)
    {
      // Has minimum required size
      if(segments.Length != 7)
      {
        return false;
      }

      // Line has values (not necesarily valid) in the required segments
      if(IsNullOrEmpty(segments[0]) || IsNullOrEmpty(segments[1]) || IsNullOrEmpty(segments[6]))
      {
        return false;
      }
      return true;
    }

    private static bool IsNullOrEmpty(string text)
    {
      return text == null || text == string.Empty;
    }

    private static bool HasValidCoordinates(string coordinates)
    {
      string[] splitted = coordinates.Split(',');

      if(splitted.Length != 2)
      {
        return false;
      }

      string latitudText = splitted[0];
      string longitudeText = splitted[1];

      decimal latitud;
      if(!decimal.TryParse(latitudText, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out latitud))
      {
        return false;
      }

      decimal longitud;
      if(!decimal.TryParse(longitudeText, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out longitud))
      {
        return false;
      }

      if(!(latitud < 5.2m && latitud > 4.4m))
      {
        return false;
      }

      if(!(longitud > -75m && longitud < -74m))
      {
        return false;
      }
      return true;
    }
  }
}
