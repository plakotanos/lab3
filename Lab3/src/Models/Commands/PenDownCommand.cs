namespace Lab3.Models.Commands;

using Lab3.Commands;

public class PenDownCommand : ICommand
{
	private readonly Turtle _turtle;

	public string Name => "pd";

	public PenDownCommand(Turtle turtle)
	{
		_turtle = turtle;

	}

    public void Execute(params string[] args)
    {
		_turtle.PenDown();
		_turtle.PrintState();
    }

	public override string ToString() => $"{Name}: command to put down the pen.";
    }
