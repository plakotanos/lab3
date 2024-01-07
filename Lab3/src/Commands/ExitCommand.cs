using System.Diagnostics.CodeAnalysis;

namespace Lab3.Commands
{
    public class ExitCommand : ICommand
    {
        public string Name => "exit";

		[DoesNotReturn]
        public void Execute(params string[] args)
        {
			Environment.Exit(0);
        }

		public override string ToString() => $"{Name}: command to exit the program.";
    }
}