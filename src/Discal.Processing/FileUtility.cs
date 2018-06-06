using System.IO;

namespace Discal.Processing
{
  public static class FileUtility
  {
    public static void Delete(string filePath)
    {
      if(File.Exists(filePath))
      {
        File.Delete(filePath);
      }
    }

    public static void WriteLine(string msg, string filePath)
    {
      if(!File.Exists(filePath))
      {
        using(StreamWriter file = new StreamWriter(filePath))
        {
          file.WriteLine(msg);
          file.Close();
        }
      }
      else
      {
        using(StreamWriter file = File.AppendText(filePath))
        {
          file.WriteLine(msg);
          file.Close();
        }
      }
    }

    public static void Write(string msg, string filePath)
    {
      if(!File.Exists(filePath))
      {
        using(StreamWriter file = new StreamWriter(filePath))
        {
          file.WriteLine(msg);
          file.Close();
        }
      }
      else
      {
        using(StreamWriter file = File.AppendText(filePath))
        {
          file.WriteLine(msg);
          file.Close();
        }
      }
    }
  }
}
