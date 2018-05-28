using System;
using Discal.Console.Modules;
using Discal.Console.State;

namespace DiscalConsole
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine("*********************************************************************************");

      Console.WriteLine("Initializing system state manager");
      var state = new StateManager();

      Console.WriteLine("Running import ...");
      var importModule = new ImportModule();
      importModule.Run(state);

      state.Save();
      Console.WriteLine("Saved system state ...");

      Console.ReadKey();
    }
  }
}
