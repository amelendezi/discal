using System.Collections.Generic;
using Discal.Orchestration;

namespace Discal.Input
{
  public interface IReader
  {
    IEnumerable<Foundation> Read(string filePath, ILogger logger);
  }
}
