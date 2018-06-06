using System.Text;

namespace Discal.Model
{
  public class GeoComparison
  {
    public Foundation Source { get; set; }

    public Foundation Target { get; set; }

    public GeoData Geo { get; set; }

    public string GetFileName()
    {
      return $"{Source.Nit}_{Target.Nit}.txt";
    }

    public string GetGeoSummary()
    {
      StringBuilder builder = new StringBuilder();
      builder.AppendLine("--------------------------------------------------------------------------------------------------------------------------");
      builder.AppendLine($"Origen      : {Source.Nit}");
      builder.AppendLine($"Fundacion   : {Source.Description}");
      builder.AppendLine($"Direccion   : {Source.Address}");
      builder.AppendLine($"Barrio      : {Source.Neighborhood}");
      builder.AppendLine($"Localidad   : {Source.Location}");
      builder.AppendLine($"Coordenadas : {Source.Coordinates}");
      builder.AppendLine("--------------------------------------------------------------------------------------------------------------------------");
      builder.AppendLine($"Destino     : {Target.Nit}");
      builder.AppendLine($"Fundacion   : {Target.Description}");
      builder.AppendLine($"Direccion   : {Target.Address}");
      builder.AppendLine($"Barrio      : {Target.Neighborhood}");
      builder.AppendLine($"Localidad   : {Target.Location}");
      builder.AppendLine($"Coordenadas : {Target.Coordinates}");
      builder.AppendLine("--------------------------------------------------------------------------------------------------------------------------");
      builder.AppendLine($"Distancia   : {Geo.DistanceText} ({Geo.Distance})");
      builder.AppendLine($"Duracion    : {Geo.DurationText} ({Geo.Duration})");
      return builder.ToString();
    }

    public string GetShortSummary()
    {
      return $"{Source.Coordinates},{Target.Coordinates};{Geo.Distance}";
    }
  }
}
