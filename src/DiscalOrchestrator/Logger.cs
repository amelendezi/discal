using System.IO;

namespace Discal.Orchestration
{
  public static class Logger
  {
    public static void Delete(string file)
    {
      if(File.Exists(file))
      {
        File.Delete(file);
      }
    }

    public static void Write(string msg, string filePath)
    {
      if(!File.Exists(filePath))
      {
        using(StreamWriter file = new StreamWriter(filePath))
        {
          file.Write(msg);
          file.Close();
        }
      }
      else
      {
        using(StreamWriter file = File.AppendText(filePath))
        {
          file.Write(msg);
          file.Close();
        }
      }
    }
  }
}
