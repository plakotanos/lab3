using Microsoft.EntityFrameworkCore;

namespace Lab3.Commands
{
	public interface ICommand
	{
		string Name { get; }
		
		void Execute(params string[] args);
	}
}
