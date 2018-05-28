using Discal.Common;
using Discal.Model;
using Discal.Orchestration.Model;

namespace Discal.Console.State
{
  public class StateManager
  {
    public StateManager()
    {
      IsLoaded = false;
      Model = new MainModel();
      Config = new Config()
      {
        ModelStateFilePath = @"C:\amedev\projects\discal\src\Resources\model.json",
        ConfigStateFilePath = @"C:\amedev\projects\discal\src\Resources\config.json",
        InputFilePath = @"C:\amedev\projects\discal\src\Resources\input.csv",
        Logger = new FileLogger(@"C:\amedev\projects\discal\src\Resources\mainlog.txt")
      };
    }

    public bool IsLoaded { get; }

    public MainModel Model { get; set; }

    public Config Config { get; set; }

    public void Save()
    {
      StatePersistance.Save(this);
    }
  }
}
