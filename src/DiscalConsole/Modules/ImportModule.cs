using Discal.Console.State;
using Discal.Orchestration.Model;
using Discal.Orchestration.Orchestrators;

namespace Discal.Console.Modules
{
  public class ImportModule : UserInterfaceModule
  {
    private Config _config;
    private ImportOrchestrator _orchestrator;

    public ImportModule()
    {
      _orchestrator = new ImportOrchestrator();
      _config = new Config()
      {

      };
    }

    public void Run(StateManager state)
    {
      PrintHeader();
      _orchestrator.Run(_config, state.Model);
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
      NextLine();
    }
  }
}
