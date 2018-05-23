using Discal.Console.State;
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

    public void Run(StateManager state)
    {
      PrintHeader(state.Config.InputFilePath);
      _orchestrator.Run(state.Config, state.Model);
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
