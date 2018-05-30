using System.IO;
using Discal.Common;
using Discal.Model;
using Newtonsoft.Json;

namespace Discal.Console.State
{
  public class StateManager
  {
    private static string ConfigFile = @".\config.json";

    public StateManager()
    {
      Model = new MainModel();
    }

    public MainModel Model { get; set; }

    public Config Config { get; private set; }

    public void Save()
    {
      StatePersistance.Save(this);
    }

    public void LoadConfig()
    {
      using(StreamReader r = new StreamReader(ConfigFile))
      {
        string json = r.ReadToEnd();
        Config = JsonConvert.DeserializeObject<Config>(json);
      }
    }

    public void Load()
    {

    }
  }
}
