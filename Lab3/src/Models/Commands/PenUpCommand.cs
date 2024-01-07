namespace Lab3.Models.Commands;

using Lab3.Commands;

public class PenUpCommand : ICommand
{
	private readonly Turtle _turtle;

	public string Name => "pu";

	public PenUpCommand(Turtle turtle)
	{
		_turtle = turtle;
	}

    public void Execute(params string[] args)
    {
		_turtle.PenUp();
		_turtle.PrintState();
    }

	public override string ToString() => $"{Name}: command to put up the pen.";
}
