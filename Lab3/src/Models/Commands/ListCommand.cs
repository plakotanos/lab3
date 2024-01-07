namespace Lab3.Models.Commands;

using Lab3.Commands;

public class ListCommand : ICommand
{
	public string Name => "list";

	private readonly Turtle _turtle;

	public ListCommand(Turtle turtle)
	{
		_turtle = turtle;
	}

	public void Execute(params string[] args)
	{
		if (args.Length != 1)
		{
			throw new ExecutionException($"'{Name}' accepts exactly one argument");
		}

		string arg = args[0].ToLower();

		if (arg == "steps")
		{
			_turtle.PrintSteps();
			return;
		}

		if (arg == "figures")
		{
			_turtle.PrintFigures();
			return;
		}

		throw new ExecutionException($"'{Name}' accepts either 'steps' or 'figures'.");
	}

	public override string ToString() {
		string result = $"{Name} steps: command to show all executed steps.\n";
		result += $"\t{Name} figures: command to show all properties of completed figures.";

		return result;
	}
}
