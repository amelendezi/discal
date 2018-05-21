namespace Discal.Console.Modules
{
  public abstract class UserInterfaceModule
  {
    public void SectionLine()
    {
      System.Console.WriteLine("*********************************************************************************");
    }

    public void NextLine()
    {
      System.Console.WriteLine("");
    }

    public void Continue()
    {
      NextLine();
      System.Console.Write("Presione cualquier tecla para continuar: ");
      System.Console.ReadKey();
    }

    public void Clear()
    {
      System.Console.Clear();
    }
  }
}
