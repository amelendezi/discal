using System.Collections.Generic;
using Discal.Input.DomainModel;
using Discal.Orchestration;

namespace Discal.Input.Reader
{
  public interface IReader
  {
    IEnumerable<Foundation> Read(string filePath, ILogger logger);
  }
}
