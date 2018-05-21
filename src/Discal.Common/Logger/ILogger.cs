using System.Collections.Generic;

namespace Discal.Common
{
  public interface ILogger
  {
    void Log(string message);

    void Commit();

    IReadOnlyCollection<string> GetLog();
  }
}
