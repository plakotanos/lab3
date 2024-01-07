using System.Xml.Serialization;
using Lab3.Commands;

namespace Lab3.Data
{
    public class SaveXmlCommand<T>(T t) : ICommand
        where T : class, ILoadable<T>
    {
        public string Name => "save-xml";

        private readonly XmlSerializer xml = new(typeof(T));

        public void Execute(params string[] args)
        {
            if (args.Length != 1)
            {
                throw new ExecutionException($"'{Name}' accepts exactly one argument.");
            }

            try {
                string path = Path.GetFullPath(args[0]);
                using var fs = File.OpenWrite(path);

                xml.Serialize(fs, t);

                Console.WriteLine($"Saved current state to '{path}'");
            }
            catch (SystemException e)
            {
                throw new ExecutionException(e.Message);
            }
        }

        public override string ToString() => $"{Name} FILENAME: command to save to XML file FILENAME.";
    }
}