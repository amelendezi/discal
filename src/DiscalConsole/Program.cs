using System;
using Discal.Console;
using Discal.Model;
using Discal.Orchestration.Orchestrators;
using Discal.Processing;

namespace DiscalConsole
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var state = new StateManager();

      string option = null;
      if(args.Length > 0)
      {
        option = args[0];
      }

      switch(option)
      {
        case "config":
          Initializer.ProductionConfig();
          Console.WriteLine("\n\rGenerted config.json file.\n\r");
          Console.ReadKey();
          break;

        case "import":
          LoadAndImport(state);
          StatisticsCalculator.GenerateStatisticsFile(state);
          Console.WriteLine("\n\rImport and load has been performed from the csv file.\n\r");
          Console.ReadKey();
          break;

        case "stats":
          LoadAndImport(state);
          StatisticsCalculator.GenerateStatisticsFile(state);
          Console.WriteLine("");
          Console.WriteLine(StatisticsCalculator.GetStatistics(state));
          break;

        case "export":
          LoadAndImport(state);
          StatisticsCalculator.GenerateStatisticsFile(state);
          Exporter.ExportText(state);
          Exporter.FullExport(state);
          Exporter.ExportJson(state);
          break;

        case "run":
          LoadAndImport(state);
          StatisticsCalculator.GenerateStatisticsFile(state);
          GoogleApiOrchestrator.Run(state);
          break;

        default:
          Console.WriteLine("\n\rProvide one of the following command line argument:");
          Console.WriteLine("\n\rconfig : generate config file");
          Console.WriteLine("import : do only import from csv file");
          Console.WriteLine("stats  : show statistics");
          Console.WriteLine("run    : execute the api caller\n\r");
          Console.WriteLine("export : export results to txt and json");
          break;
      }
    }

    private static void LoadAndImport(StateManager state)
    {
      // Load config      
      state.LoadConfig();

      // Load Model      
      if(!state.LoadModel())
      {
        state.Model = ImportOrchestrator.Run(state.Config.ImportFilePath);
        state.SaveModel();
      }
    }
  }
}
