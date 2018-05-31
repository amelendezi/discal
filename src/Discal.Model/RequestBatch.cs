using System.Collections.Generic;

namespace Discal.Model
{
  public class RequestBatch
  {
    public RequestBatch(Foundation foundation)
    {
      Status = "pending";
      HasBeenProcessed = false;
      Foundation = foundation;
      FoundationsToCompareAgainst = new List<Foundation>();
    }

    public Foundation Foundation { get; set; }

    public List<Foundation> FoundationsToCompareAgainst { get; set; }

    public string RequestUrl { get; set; }

    public bool HasBeenProcessed { get; set; }

    public string Status { get; set; }
  }
}
