namespace Lab3.Data;

using System.Xml.Serialization;
using Lab3.Commands;

public class LoadXmlCommand<T>(T t) : ICommand
    where T : class, ILoadable<T>
{
    public string Name => "load-xml";

    private readonly XmlSerializer xml = new(typeof(T));

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

            object? value = xml.Deserialize(fs)
                 ?? throw new ExecutionException("Could not load state from file.");

            t.LoadFrom((T)value);
        }
        catch (SystemException e)
        {
            throw new ExecutionException(e.Message);
        }

        Console.WriteLine("Loaded successfully.");
    }

    public override string ToString() => $"{Name} FILENAME: command to load from XML file FILENAME.";
}
