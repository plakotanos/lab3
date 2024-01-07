namespace Lab3.Models.Commands;

using Lab3.Commands;

public class ColorCommand : ICommand
{
	public string Name => "color";

	private readonly Turtle _turtle;
	private readonly string _possibleColorsString = string.Join(
		", ",
		Turtle.PossibleColors.Select(c => $"\"{c.ToString().ToLower()}\"")
	);

	public ColorCommand(Turtle turtle)
	{
		_turtle = turtle;
	}

	public void Execute(params string[] args)
	{
		if (args.Length != 1)
		{
			throw new ExecutionException($"'{Name}' accepts exactly one argument.");
		}

		PenColor? color = args[0].ToPenColor()
			?? throw new ExecutionException($"Color possible values: {_possibleColorsString}.");

        _turtle.Color = (PenColor)color;
		_turtle.PrintState();
	}

	public override string ToString()
		=> $"{Name} COLOR: command to change turtle's color "
		+ $"of the pen to COLOR. Possible values: {_possibleColorsString}";
	}
