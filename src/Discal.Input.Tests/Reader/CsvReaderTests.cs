using System.Collections.Generic;
using System.Linq;
using Discal.Common;
using NUnit.Framework;

namespace Discal.Input.Tests
{
  [TestFixture]
  public class CsvReaderTests
  {
    public static string CorrectTestFile = @"C:\amedev\projects\discal\src\Resources\test\input_test_correct.csv";
    public static string IncorrectTestFile = @"C:\amedev\projects\discal\src\Resources\test\input_test_incorrect.csv";
    public static string IncompleteTestFile = @"C:\amedev\projects\discal\src\Resources\test\input_test_incomplete.csv";

    [Test]
    public void ReadFromCsvFile_WithCorrectData_ExpectsFoundationModelInstances_WithCorrectValues()
    {
      IReader reader = new CsvReader();
      ILogger logger = new FileLogger(@".\InputErrorLog.txt");
      IEnumerable<Foundation> foundations = reader.Read(CorrectTestFile, logger);

      Assert.AreEqual(expected: 5, actual: foundations.Count());
      Assert.AreEqual(expected: true, actual: foundations.Any(f => f.Number == 1 && f.Nit == "827000274" && f.Coordinates == "4.670127,-74.110777"));
      Assert.AreEqual(expected: true, actual: foundations.Any(f => f.Number == 2 && f.Nit == "800114682" && f.Coordinates == "4.672477,-74.070736"));
      Assert.AreEqual(expected: true, actual: foundations.Any(f => f.Number == 3 && f.Nit == "900376067" && f.Coordinates == "4.709163,-74.055679"));
      Assert.AreEqual(expected: true, actual: foundations.Any(f => f.Number == 4 && f.Nit == "900423978" && f.Coordinates == "4.579383,-74.153807"));
      Assert.AreEqual(expected: true, actual: foundations.Any(f => f.Number == 5 && f.Nit == "900226180" && f.Coordinates == "4.686935,-74.150768"));
      Assert.AreEqual(expected: 0, actual: logger.GetLog().Count);
    }

    [Test]
    public void ReadFromCsvFile_WithIncompletButCorrectData_ExpectsFoundationModelInstances_WithMinimumRequiredValues()
    {
      IReader reader = new CsvReader();
      ILogger logger = new FileLogger(@".\InputErrorLog.txt");
      IEnumerable<Foundation> foundations = reader.Read(IncompleteTestFile, logger);

      Assert.AreEqual(expected: 3, actual: foundations.Count());
      Assert.AreEqual(expected: true, actual: foundations.Any(f => f.Number == 1 && f.Nit == "827000274" && f.Coordinates == "4.670127,-74.110777"));
      Assert.AreEqual(expected: true, actual: foundations.Any(f => f.Number == 2 && f.Nit == "800114682" && f.Coordinates == "4.672477,-74.070736"));
      Assert.AreEqual(expected: true, actual: foundations.Any(f => f.Number == 3 && f.Nit == "900376067" && f.Coordinates == "4.709163,-74.055679"));
      Assert.AreEqual(expected: 0, actual: logger.GetLog().Count);
    }

    [Test]
    public void ReadFromCsvFile_WithIncorrectData_ExpectsFoundationsToNotBeCreatedAndLinesToBeIgnored()
    {
      IReader reader = new CsvReader();
      ILogger logger = new FileLogger(@".\InputErrorLog.txt");
      IEnumerable<Foundation> foundations = reader.Read(IncorrectTestFile, logger);

      Assert.AreEqual(expected: 0, actual: foundations.Count());
      Assert.AreEqual(expected: 6, actual: logger.GetLog().Count);
    }
  }
}
