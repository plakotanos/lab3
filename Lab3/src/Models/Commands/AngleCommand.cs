namespace Lab3.Models.Commands;

using Lab3.Commands;

public class AngleCommand(Turtle turtle) : ICommand
{
	public string Name => "angle";

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

		turtle.Turn(n);
		turtle.PrintState();
	}

	public override string ToString() => $"{Name} N: command to change turtle's angle of direction to N degrees.";
}
