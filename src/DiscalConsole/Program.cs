using System;
using Discal.Console.Modules;
using Discal.Console.State;

namespace DiscalConsole
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var state = new StateManager();

      Console.WriteLine("Attempting to load config file ... ");
      state.LoadConfig();
      Console.WriteLine("Config has been loaded ... ");
      Console.ReadKey();

      Console.WriteLine("Attempting to load import data ...");

      Console.WriteLine("Import data has been loaded ...");
      Console.ReadKey();



      Console.WriteLine("Running import ...");
      var importModule = new ImportModule();
      // importModule.Run(state.Model, state.Config);

      state.Save();
      Console.WriteLine("Saved system state ...");

      Console.ReadKey();
    }

    private static void LoadConfig()
    {

    }
  }
}
