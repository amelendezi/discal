using System.Collections.Generic;

namespace Discal.Model
{
  public class Config
  {
    public string ImportFilePath { get; set; }

    public string ModelFilePath { get; set; }

    public string LogFilePath { get; set; }

    public List<ApiKey> GoogleApiKeys { get; set; }
  }
}
