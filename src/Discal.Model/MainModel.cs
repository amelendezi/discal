using System.Collections.Generic;

namespace Discal.Model
{
  public class MainModel
  {
    public Foundation[] Foundations { get; set; }

    public List<RequestBatch> RequestBatches { get; set; }

    public List<GeoComparison> GeoComparisons { get; set; }
  }
}
