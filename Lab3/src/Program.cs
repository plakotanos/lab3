using Lab3.Commands;
using Lab3.Data;
using Lab3.Models;
using Lab3.Models.Commands;

TurtleContext context = new();

CommandExecutor executor = new();

Turtle turtle = context.Turtles.FirstOrDefault() ?? new Turtle();

executor.AddCommands(
	new AngleCommand(turtle),
	new ColorCommand(turtle),
	new ListCommand(turtle),
	new MoveCommand(turtle),
	new PenDownCommand(turtle),
	new PenUpCommand(turtle),
	new SaveJsonCommand<Turtle>(turtle),
	new LoadJsonCommand<Turtle>(turtle),
	new SaveXmlCommand<Turtle>(turtle),
	new LoadXmlCommand<Turtle>(turtle)
);



executor.Help();
while (true)
{
	try
	{
		if (!executor.TryExecuteCommand())
		{
			break;
		}
	}
	catch (ExecutionException e)
	{
		Console.Error.WriteLine($"Error: {e.Message}");
	}
}
