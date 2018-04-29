using System.Collections.Generic;
using System.IO;

namespace Discal.Orchestration
{
  public class FileLogger : ILogger
  {
    private string _fileName;
    private List<string> _messages;

    public FileLogger(string fileName)
    {
      _messages = new List<string>();
      _fileName = fileName;
    }

    public void Log(string message)
    {
      _messages.Add(message);
    }

    public void Commit()
    {
      using(StreamWriter file = new StreamWriter(_fileName))
      {
        foreach(string message in _messages)
        {
          file.WriteLine(message);
        }
        file.Close();
      }
    }

    public IReadOnlyCollection<string> GetLog()
    {
      return _messages.AsReadOnly();
    }
  }
}
