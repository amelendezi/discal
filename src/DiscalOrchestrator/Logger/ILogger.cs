using System.Collections.Generic;

namespace Discal.Orchestration
{
  public interface ILogger
  {
    void Log(string message);

    void Commit();

    IReadOnlyCollection<string> GetLog();
  }
}
