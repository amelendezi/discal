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
      if(!File.Exists(_fileName))
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
      else
      {
        using(StreamWriter file = File.AppendText(_fileName))
        {
          foreach(string message in _messages)
          {
            file.WriteLine(message);
          }
          file.Close();
        }
      }
      _messages.Clear();
    }

    public IReadOnlyCollection<string> GetLog()
    {
      return _messages.AsReadOnly();
    }
  }
}
