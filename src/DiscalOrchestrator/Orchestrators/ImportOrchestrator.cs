using System.Linq;
using Discal.Common;
using Discal.Input;
using Discal.Model;
using Discal.Processing.Builders;

namespace Discal.Orchestration.Orchestrators
{
  public class ImportOrchestrator
  {
    public void Run(Config config, MainModel model)
    {
      Foundation[] foundations = CsvReader.Read(config.ImportFilePath, new FileLogger(config.LogFilePath)).ToArray();
      model.Foundations = foundations;
      model.RequestBatches = RequestBatchBuilder.BuildRequestBatches(foundations);
    }
  }
}
