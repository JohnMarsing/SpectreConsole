using Spectre.Console;
using SpectreConsole;
using System.Collections.Generic;


class Program
{
	class ProgressInfo
	{
		public double BookTaskIncrease { get; set; }
	}

	class AnsiConsoleProgress
	{
		private readonly ProgressTask _taskBook;

		private readonly object _consoleLock = new object();

		public AnsiConsoleProgress(ProgressTask taskBook)
		{
			_taskBook = taskBook;
		}

		public void ReportProgress(ProgressInfo info)
		{
			lock (_consoleLock)
			{
				_taskBook.Increment(info.BookTaskIncrease);
			}
		}
	}

	public static async Task Main(string[] args)
	{
		AnsiConsole.MarkupLine("[blue]Start[/]");

    //Column.ShowOtBooks(BookGroupEnum.Torah);
    //Column.ShowOtBooks(BookGroupEnum.History);
    //Column.ShowOtBooks(BookGroupEnum.Poetry);
    //Column.ShowOtBooks(BookGroupEnum.MajorProphets);
    //Column.ShowOtBooks(BookGroupEnum.MinorProphets);

    //AnsiConsole.WriteLine();

    try
		{
			if (Prompt.RaiseException())
			{
				int zero = 0;
				int result = 1 / zero;
			}

			await AnsiConsole.Progress()
					.StartAsync(async ctx =>
					{
						var task1 = ctx.AddTask("[green]Porting Book[/]"); // Define tasks
						var ansiProgress = new AnsiConsoleProgress(task1);
						var progress = new Progress<ProgressInfo>(ansiProgress.ReportProgress);
						await ProcessBibleByBook_OT_Only(progress);
					});
		}
		catch (Exception ex)
		{
      AnsiConsole.WriteLine();
      AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
      AnsiConsole.WriteLine();
    }

		finally
		{
      AnsiConsole.Write(
        new FigletText("Finished")
        .Centered()
        .Color(Color.Aqua));
    }

		//AnsiConsole.MarkupLine("[blue]End[/]");

	}

	private static async Task ProcessBibleByBook_OT_Only(IProgress<ProgressInfo> progress)
	{
		int counter = 0;

		while (counter < BibleBook.Matthew.Value)
		{
			await Task.Delay(150);
			counter++;
			progress.Report(new ProgressInfo { BookTaskIncrease = 2.5 });
		}
	}


}

