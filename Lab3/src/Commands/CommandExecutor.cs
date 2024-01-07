using System.Text.RegularExpressions;

namespace Lab3.Commands
{
	public class CommandExecutor
	{
		private readonly Dictionary<string, ICommand> _commands = new();

		public CommandExecutor()
		{
			AddCommands(
				new HelpCommand(_commands.Values),
				new ExitCommand()
			);
		}

		public void AddCommands(params ICommand[] commands)
		{
			foreach (var command in commands)
			{
				_commands[command.Name] = command;
			}
		}

		public void Help()
		{
			_commands["help"].Execute();
		}

		public bool TryExecuteCommand()
		{
			Console.Write("> ");
			string? input = Console.ReadLine();

			if (input == null)
			{
				return false;
			}

			string[] parts = Regex.Split(input.Trim(), @"\s+");
			string? name = parts.ElementAtOrDefault(0);

			if (name == null)
			{
				return true;
			}

			if (_commands.TryGetValue(name, out var command))
			{
				string[] args = parts.Skip(1).ToArray();
				command.Execute(args);
			}
			else
			{
				throw new ExecutionException("Unknown command. Use 'help' to see all commands.");
			}

			return true;
		}

		public static int? ParseIntArg(string arg)
		{
			if (int.TryParse(arg, out int result))
			{
				return result;
			}

			return null;
		}
	}
}
