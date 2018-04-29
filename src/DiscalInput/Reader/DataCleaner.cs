using System;
using System.Text.RegularExpressions;

namespace Discal.Input.Reader
{
  public static class DataCleaner
  {
    public static void CleanFoundation(Foundation foundation)
    {
      foundation.Nit = CleanText(foundation.Nit);
      foundation.Description = CleanText(foundation.Description);
      foundation.Neighborhood = CleanText(foundation.Neighborhood);
      foundation.Location = CleanText(foundation.Location);
      foundation.Address = CleanText(foundation.Address);
      foundation.Coordinates = CleanText(foundation.Coordinates);
    }

    private static string CleanText(string text)
    {
      string result = text.Replace("\"", String.Empty);
      result = Regex.Replace(result, @"( |\r?\n)\1+", "$1").Trim();
      return result;
    }
  }
}
