using Discal.Common;
using Discal.Model;
using Discal.Orchestration.Orchestrators;

namespace Discal.Console.Modules
{
  public class ImportModule : UserInterfaceModule
  {
    private ImportOrchestrator _orchestrator;

    public ImportModule()
    {
      _orchestrator = new ImportOrchestrator();
    }

    public void Run(MainModel model, Config config)
    {
      PrintHeader(config.ImportFilePath);
      _orchestrator.Run(config, model);
    }

    private void PrintHeader(string inputFilePath)
    {
      SectionLine();
      System.Console.WriteLine("Modulo de Importacion de Datos");
      NextLine();
      System.Console.WriteLine($"Archivo de Importacion: {inputFilePath}");
      SectionLine();
      NextLine();
      Continue();
      NextLine();
    }
  }
}
