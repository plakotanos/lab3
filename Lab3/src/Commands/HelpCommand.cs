using System.Text;

namespace Lab3.Commands
{
	public class HelpCommand : ICommand
	{
		public string Name => "help";

		private readonly ICollection<ICommand> _commands;

		public HelpCommand(ICollection<ICommand> commands)
		{
			_commands = commands;
		}

		public void Execute(params string[] args)
		{
			StringBuilder builder = new("Commands:\n");

			foreach (var command in _commands)
			{
				builder.AppendLine($"\t{command.ToString()}");
			}

			Console.WriteLine(builder);
		}

		public override string ToString() => $"{Name}: command to display this message.";
	}
}
