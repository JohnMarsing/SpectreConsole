using Spectre.Console;

namespace SpectreConsole;

public static class Prompt
{
	public static bool RaiseException()
	{
		if (AnsiConsole.Confirm("Test exception logic?"))
		{
			AnsiConsole.MarkupLine("Ok, this might hurt  :(");
			return true;
		}
		else 
		{
			AnsiConsole.MarkupLine("Nope, happy path only  :)");
			return false;
		}
		
		
	}

}

// Ignore Spelling: Figle