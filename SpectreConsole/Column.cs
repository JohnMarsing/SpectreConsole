using Spectre.Console;

namespace SpectreConsole;

public static class Column
{
	public static void ShowOtBooks(BookGroupEnum bookGroupEnum)
	{
		var cards = new List<Panel>();
		foreach (var item in BibleBook.List.Where(w => w.BookGroupEnum == bookGroupEnum).OrderBy(o => o.Value).ToList())
		{
			cards.Add(
				new Panel(item.TransliterationInHebrew)
						.Header($"{item.Abrv}")
						.RoundedBorder().Expand());
		}

    Ruler.HL(bookGroupEnum);
    AnsiConsole.Write(new Spectre.Console.Columns(cards));

	}
}
