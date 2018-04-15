using System.Collections.Generic;
using Discal.Input.DomainModel;

namespace Discal.Input.Reader
{
  public interface IReader
  {
    IEnumerable<Foundation> Read(string filePath);
  }
}
