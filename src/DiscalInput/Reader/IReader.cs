using System.Collections.Generic;
using Discal.Common;
using Discal.Model;

namespace Discal.Input
{
  public interface IReader
  {
    IEnumerable<Foundation> Read(string filePath, ILogger logger);
  }
}
