using Discal.Orchestration.Model;
using Discal.Orchestration.Orchestrators;

namespace Discal.Console.Modules
{
  public class ImportModule : UserInterfaceModule
  {
    private ImportConfig _config;

    public ImportModule()
    {
      _config = new ImportConfig()
      {
        InputFilePath = @"C:\amedev\projects\discal\src\Resources\input.csv"
      };
    }

    public void Run()
    {
      var orchestrator = new ImportOrchestrator();
    }

    private void PrintHeader()
    {
      SectionLine();
      System.Console.WriteLine("Modulo de Importacion de Datos");
      NextLine();
      System.Console.WriteLine($"Archivo de Importacion: {_config.InputFilePath}");
      SectionLine();
      NextLine();
      Continue();
    }
  }
}
