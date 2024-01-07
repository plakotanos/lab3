using Lab3.Data;

namespace Lab3Test;

public class Data : ILoadable<Data>
{
    public string Foo { get; set; } = "";
    public int Bar { get; set; } = 0;
    
    public void LoadFrom(Data source)
    {
        Foo = new string(source.Foo);
        Bar = source.Bar;
    }
}
