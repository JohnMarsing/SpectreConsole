using Spectre.Console;
using SpectreConsole;

/*
- https://github.com/spectreconsole/spectre.console/discussions/162
- [What does a lock statement do under the hood?](https://stackoverflow.com/questions/6029804/what-does-a-lock-statement-do-under-the-hood)
	- generally preferred over using the Monitor class directly
- [`Progress`](https://learn.microsoft.com/en-us/dotnet/api/system.progress-1?view=net-7.0) invokes callbacks for each reported progress value.
 */

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
			AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
		}

		finally
		{
			//AnsiConsole.MarkupLine("[blue]Finished[/]");
			AnsiConsole.Write(
				new FigletText("Finished")
				.Centered()
				.Color(Color.Aqua));
		}


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

