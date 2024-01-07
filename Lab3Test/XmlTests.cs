using Lab3.Commands;
using Lab3.Data;

namespace Lab3Test;

public class XmlTests
{
    private Data _data = null!;
    private LoadXmlCommand<Data> _load = null!;
    private SaveXmlCommand<Data> _save = null!;
    
    [SetUp]
    public void Setup()
    {
        _data = new Data();
        _load = new LoadXmlCommand<Data>(_data);
        _save = new SaveXmlCommand<Data>(_data);
    }

    [Test]
    public void LoadCommand_LoadsObjectCorrectly()
    {
        var path = Path.GetTempFileName() + ".xml";

        using (var fs = File.OpenWrite(path))
        {
            fs.Write("<?xml version='1.0'?><Data><Foo>FOO</Foo><Bar>123</Bar></Data>"u8);
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
        var path = Path.GetTempFileName() + ".xml";

        using (var fs = File.OpenWrite(path))
        {
            fs.Write("not xml"u8);
        }

        Assert.Throws<ExecutionException>(() => _load.Execute(path));
    }
    
    [Test]
    public void LoadCommand_ThrowsWhenFileNotAccessible()
    {
        const string path = "/this/path/does/not/exist.xml";
        
        Assert.Throws<ExecutionException>(() => _load.Execute(path));
    }

    [Test]
    public void SaveCommand_SavesObjectCorrectly()
    {
        var path = Path.GetTempFileName() + ".xml";
        var loadData = new Data();
        var load = new LoadXmlCommand<Data>(loadData);
        
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
        const string path = "/this/path/does/not/exist.xml";
        
        Assert.Throws<ExecutionException>(() => _save.Execute(path));
    }
}