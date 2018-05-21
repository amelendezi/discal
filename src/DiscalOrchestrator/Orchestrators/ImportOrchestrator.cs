using System.Linq;
using Discal.Input;
using Discal.Model;
using Discal.Orchestration.Model;

namespace Discal.Orchestration.Orchestrators
{
  public class ImportOrchestrator
  {
    public void Run(Config config, MainModel model)
    {
      var reader = new CsvReader();
      model.Foundations = reader.Read(config.InputFilePath, config.Logger).ToList();
    }
  }
}
