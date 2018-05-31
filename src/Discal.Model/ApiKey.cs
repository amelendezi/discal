using System;

namespace Discal.Model
{
  public class ApiKey
  {
    public DateTime LastDateOfUse { get; set; }

    public string Value { get; set; }

    public bool Active { get; set; }
  }
}
