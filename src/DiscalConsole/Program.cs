using System;
using System.Net.Mime;
using Discal.Console;
using Discal.Model;
using Discal.Orchestration.Orchestrators;

namespace DiscalConsole
{
  public class Program
  {
    public static void Main(string[] args)
    {
      if(args.Length > 0)
      {
        string option = args[0];
        switch(option)
        {
          case "prod":
            Initializer.ProductionConfig();
            break;
          case "dev":
            Initializer.DevConfig();
            break;
        }
      }
      else
      {
        var state = new StateManager();
        LoadAndImport(state);
        GoogleApiOrchestrator.Run(state);
      }
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
        state.SaveModel();
        Console.WriteLine("Import from data has been done and saved ...");
      }
      else
      {
        Console.WriteLine("Model has been loaded ...");
      }
    }
  }
}
