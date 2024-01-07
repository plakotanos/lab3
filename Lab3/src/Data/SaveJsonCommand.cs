using System.Text.Json;
using Lab3.Commands;
using Lab3.Models;

namespace Lab3.Data
{
    public class SaveJsonCommand<T>(T t) : ICommand
        where T : class, ILoadable<T>
    {
        private JsonSerializerOptions options = new() { WriteIndented = true };

        public string Name => "save-json";
        
        public void Execute(params string[] args)
        {
            if (args.Length != 1)
            {
                throw new ExecutionException($"'{Name}' accepts exactly one argument.");
            }

            try {
                string path = Path.GetFullPath(args[0]);
                using var fs = File.OpenWrite(path);

                JsonSerializer.Serialize(fs, t, options);

                Console.WriteLine($"Saved current state to '{path}'");
            }
            catch (SystemException e)
            {
                throw new ExecutionException(e.Message);
            }
        }

        public override string ToString() => $"{Name} FILENAME: command to save to JSON file FILENAME.";
    }
}