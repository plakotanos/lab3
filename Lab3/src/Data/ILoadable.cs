namespace Lab3.Data;

public interface ILoadable<T> where T : class
{
    void LoadFrom(T source);
}
