using System.Linq;
using Discal.Input;
using Discal.Model;
using Discal.Orchestration.Model;
using Discal.Processing.Builders;

namespace Discal.Orchestration.Orchestrators
{
  public class ImportOrchestrator
  {
    public void Run(Config config, MainModel model)
    {
      Foundation[] foundations = CsvReader.Read(config.InputFilePath, config.Logger).ToArray();
      model.Foundations = foundations;
      model.RequestBatches = RequestBatchBuilder.BuildRequestBatches(foundations);
    }
  }
}
