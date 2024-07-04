using Spectre.Console;

namespace SpectreConsole;

public static class Ruler
{
  public static void HL(BookGroupEnum bookGroupEnum)
  {
    var rule = new Rule($"[green]{bookGroupEnum}[/]")
                    .RuleStyle(Style.Parse("green"))
                    .HeavyBorder()
                    .Centered();

    AnsiConsole.WriteLine();
    AnsiConsole.WriteLine();
    AnsiConsole.Write(rule);

  }
}