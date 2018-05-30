using System.Linq;
using Discal.Input;
using Discal.Model;
using Discal.Processing.Builders;

namespace Discal.Orchestration.Orchestrators
{
  public static class ImportOrchestrator
  {
    public static MainModel Run(string importFilePath)
    {
      Foundation[] foundations = CsvReader.Read(importFilePath).ToArray();

      return new MainModel()
      {
        Foundations = foundations,
        RequestBatches = RequestBatchBuilder.BuildRequestBatches(foundations)
      };
    }
  }
}
