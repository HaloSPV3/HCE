using static System.Environment;
using static System.Environment.SpecialFolder;
using static System.IO.Path;

namespace SPV3
{
  public class Paths
  {
    public const string Compile = "0xCOMPILE";

    public static readonly string Directory     = Combine(GetFolderPath(ApplicationData), "SPV3");
    public static readonly string Exception     = Combine(Directory,                      "exception.log");
    public static readonly string Configuration = Combine(Directory,                      "loader.bin");
    public static readonly string DOOM          = Combine(CurrentDirectory,               "doom.bin");
    public static readonly string Blind         = Combine(CurrentDirectory,               "blind.bin");

    public static string Packages(string target) => Combine(target, "data");
  }
}