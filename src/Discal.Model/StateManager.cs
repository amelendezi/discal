using System.IO;

namespace Discal.Model
{
  public class StateManager
  {
    private static string ConfigFile = @".\config.json";

    public MainModel Model { get; set; }

    public Config Config { get; private set; }

    public void Save()
    {
      var model = JsonConvert.SerializeObject(Model);
      Write(Config.ModelFilePath, model);
    }

    public void LoadConfig()
    {
      using(StreamReader r = new StreamReader(ConfigFile))
      {
        string json = r.ReadToEnd();
        Config = JsonConvert.DeserializeObject<Config>(json);
      }
    }

    public bool LoadModel()
    {
      if(File.Exists(Config.ModelFilePath))
      {
        using(StreamReader r = new StreamReader(Config.ModelFilePath))
        {
          string json = r.ReadToEnd();
          Model = JsonConvert.DeserializeObject<MainModel>(json);
        }
        return true;
      }
      return false;
    }

    private void Write(string outputFilePath, string content)
    {
      Cleanup(outputFilePath);
      using(StreamWriter file = new StreamWriter(outputFilePath))
      {
        file.Write(content);
      }
    }

    private void Cleanup(string outputFilePath)
    {
      if(File.Exists(outputFilePath))
      {
        File.Delete(outputFilePath);
      }
    }
  }
}
