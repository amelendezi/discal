using Discal.Common;
using Discal.Model;
using Discal.Orchestration.Model;

namespace Discal.Console.State
{
  public class StateManager
  {
    public StateManager()
    {
      Model = new MainModel();
      Config = new Config()
      {
        InputFilePath = @"C:\amedev\projects\discal\src\Resources\input.csv",
        Logger = new FileLogger(@"C:\amedev\projects\discal\src\Resources\mainlog.txt")
      };
    }

    public MainModel Model { get; set; }

    public Config Config { get; set; }
  }
}
