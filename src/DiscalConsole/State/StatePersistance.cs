using System.IO;
using Newtonsoft.Json;

namespace Discal.Console.State
{
  public static class StatePersistance
  {
    public static void Save(StateManager state)
    {
      var model = JsonConvert.SerializeObject(state.Model);
      Write(state.Config.ModelStateFilePath, model);

      var config = JsonConvert.SerializeObject(state.Config);
      Write(state.Config.ConfigStateFilePath, config);
    }

    private static void Write(string outputFilePath, string content)
    {
      Cleanup(outputFilePath);
      using(StreamWriter file = new StreamWriter(outputFilePath))
      {
        file.Write(content);
      }
    }

    private static void Cleanup(string outputFilePath)
    {
      if(File.Exists(outputFilePath))
      {
        File.Delete(outputFilePath);
      }
    }
  }
}
