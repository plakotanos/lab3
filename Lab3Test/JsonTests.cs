using Lab3.Commands;
using Lab3.Data;

namespace Lab3Test;

public class JsonTests
{
    private Data _data = null!;
    private LoadJsonCommand<Data> _load = null!;
    private SaveJsonCommand<Data> _save = null!;
    
    [SetUp]
    public void Setup()
    {
        _data = new Data();
        _load = new LoadJsonCommand<Data>(_data);
        _save = new SaveJsonCommand<Data>(_data);
    }

    [Test]
    public void LoadCommand_LoadsObjectCorrectly()
    {
        var path = Path.GetTempFileName() + ".json";

        using (var fs = File.OpenWrite(path))
        {
            fs.Write("{ \"Foo\": \"FOO\", \"Bar\": 123 }"u8);
        }

        _load.Execute(path);
        
        Assert.Multiple(() =>
        {
            Assert.That(_data.Foo, Is.EqualTo("FOO"));
            Assert.That(_data.Bar, Is.EqualTo(123));
        });
    }

    [Test]
    public void LoadCommand_ThrowsWhenBadFile()
    {
        var path = Path.GetTempFileName() + ".json";

        using (var fs = File.OpenWrite(path))
        {
            fs.Write("not json"u8);
        }

        Assert.Throws<ExecutionException>(() => _load.Execute(path));
    }
    
    [Test]
    public void LoadCommand_ThrowsWhenFileNotAccessible()
    {
        const string path = "/this/path/does/not/exist.json";
        
        Assert.Throws<ExecutionException>(() => _load.Execute(path));
    }

    [Test]
    public void SaveCommand_SavesObjectCorrectly()
    {
        var path = Path.GetTempFileName() + ".json";
        var loadData = new Data();
        var load = new LoadJsonCommand<Data>(loadData);
        
        _data.Foo = "save";
        _data.Bar = 321;
        
        _save.Execute(path);
        load.Execute(path);
        
        Assert.Multiple(() =>
        {
            Assert.That(_data.Foo, Is.EqualTo(loadData.Foo));
            Assert.That(_data.Bar, Is.EqualTo(loadData.Bar));
        });
    }

    [Test]
    public void SaveCommand_ThrowsWhenFileNotAccessible()
    {
        const string path = "/this/path/does/not/exist.json";
        
        Assert.Throws<ExecutionException>(() => _save.Execute(path));
    }
}