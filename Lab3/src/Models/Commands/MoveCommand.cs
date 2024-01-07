namespace Lab3.Models.Commands;

using Lab3.Commands;

public class MoveCommand : ICommand
{
	public string Name => "move";

	private readonly Turtle _turtle;

	public MoveCommand(Turtle turtle)
	{
		_turtle = turtle;
	}

	public void Execute(params string[] args)
	{
		if (args.Length != 1)
		{
			throw new ExecutionException($"'{Name}' accepts exactly one argument.");
		}

		if (!int.TryParse(args[0], out int n))
		{
			throw new ExecutionException($"'{Name}' does not accept non-integer arguments.");
		}

		_turtle.Move(n);
		_turtle.PrintState();
	}

	public override string ToString() => $"{Name} N: command to change turtle's angle of direction to N degrees.";
}
