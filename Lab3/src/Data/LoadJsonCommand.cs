namespace Lab3.Data;

using System.Text.Json;
using Lab3.Commands;

public class LoadJsonCommand<T>(T t) : ICommand
    where T : class, ILoadable<T>
{
    public string Name => "load-json";
    
    public void Execute(params string[] args)
    {
        if (args.Length != 1)
        {
            throw new ExecutionException($"'{Name}' accepts exactly one argument.");
        }

        try
        {
            string path = Path.GetFullPath(args[0]);
            using var fs = File.OpenRead(path);

            object? value = JsonSerializer.Deserialize(fs, typeof(T))
                 ?? throw new ExecutionException("Could not load state from file.");

            t.LoadFrom((T)value);
        }
        catch (SystemException e)
        {
            throw new ExecutionException(e.Message);
        }

        Console.WriteLine("Loaded successfully.");
    }

    public override string ToString() => $"{Name} FILENAME: command to load from JSON file FILENAME.";
}