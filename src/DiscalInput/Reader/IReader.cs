using System.Collections.Generic;
using Discal.Common;

namespace Discal.Input
{
  public interface IReader
  {
    IEnumerable<Foundation> Read(string filePath, ILogger logger);
  }
}
