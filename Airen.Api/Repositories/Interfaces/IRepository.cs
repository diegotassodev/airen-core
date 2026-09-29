namespace Airen.Api.Repositories;

public interface IRepository<T, ReadT, UpdateT>
{
    public void Create (T obj);
    public IEnumerable<ReadT> ReadAll();
    public T? ReadById(int id);
    public void Update (UpdateT dto, T obj);
    public void Delete (T obj);
}