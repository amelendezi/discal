using System.Collections.Generic;
using Discal.Model;

namespace Discal.Processing.Builders
{
  public static class RequestBatchBuilder
  {
    public const int DefaultBufferSize = 100;

    public static List<RequestBatch> BuildRequestBatches(Foundation[] foundations, int bufferSize = 0)
    {
      int buffer = bufferSize == 0 ? DefaultBufferSize : bufferSize;
      var requestBatches = new List<RequestBatch>();

      for(int i = 0; i < foundations.Length; i++)
      {
        int bufferCount = 0;
        var foundationsToCompareAgainst = new List<Foundation>();

        for(int j = (i + 1); j < foundations.Length; j++)
        {
          bufferCount++;
          foundationsToCompareAgainst.Add(foundations[j]);

          if(bufferCount == buffer || j == foundations.Length - 1)
          {
            var requestBatch = new RequestBatch(foundations[i]);
            requestBatch.FoundationsToCompareAgainst = foundationsToCompareAgainst;
            requestBatches.Add(requestBatch);

            bufferCount = 0;
            foundationsToCompareAgainst = new List<Foundation>();
          }
        }
      }
      return requestBatches;
    }
  }
}
