using System.IO;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Discal.Orchestration.Tests.Logger
{
  [TestFixture]
  public class FileLoggerTests
  {
    private const string _logFile = @"C:\amedev\projects\discal\src\Resources\test\testlog.txt";

    [Test]
    public void LogFileThatDoesNotExist_IsCreated()
    {
      CleanupLogFile();
      ILogger logger = new FileLogger(_logFile);
      logger.Log("Line 1");
      logger.Log("Line 2");
      logger.Commit();

      Assert.AreEqual(expected: true, actual: File.Exists(_logFile));
    }

    [Test]
    public void LogFileExists_LinesAreAppended()
    {
      // Prepare
      CleanupLogFile();
      ILogger logger = new FileLogger(_logFile);

      // Act
      logger.Log("Line 1");
      logger.Commit();

      logger.Log("Line 2");
      logger.Commit();

      logger.Log("Line 3");
      logger.Commit();

      // Assert
      int counter = 0;
      string line;

      StreamReader file = new System.IO.StreamReader(_logFile);
      while((line = file.ReadLine()) != null)
      {
        counter++;
      }
      file.Close();
      Assert.AreEqual(expected: 3, actual: counter);
    }

    private void CleanupLogFile()
    {
      if(File.Exists(_logFile))
      {
        File.Delete(_logFile);
      }
    }
  }
}
