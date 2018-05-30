using System;
using Discal.Console.State;
using Discal.Orchestration.Orchestrators;

namespace DiscalConsole
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var state = new StateManager();
      LoadAndImport(state);
      ExecuteGoogleApiCalls(state);
    }

    private static void LoadAndImport(StateManager state)
    {
      // Load config      
      state.LoadConfig();
      Console.WriteLine("Config has been loaded ... ");

      // Load Model      
      if(!state.LoadModel())
      {
        state.Model = ImportOrchestrator.Run(state.Config.ImportFilePath);
        state.Save();
        Console.WriteLine("Import from data has been done and saved ...");
      }
      else
      {
        Console.WriteLine("Model has been loaded ...");
      }
      Console.ReadKey();
    }

    private static void ExecuteGoogleApiCalls(StateManager state)
    {

    }
  }
}
