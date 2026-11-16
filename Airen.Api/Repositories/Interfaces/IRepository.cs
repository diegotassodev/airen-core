namespace Airen.Api.Repositories;

public interface IRepository<T, ReadT, UpdateT>
{
    public ReadT? Create (T obj);
    public IEnumerable<ReadT> ReadAll();
    public ReadT? ReadById(int id);
    public T? ReadEntityById(int id);
    public ReadT? Update (UpdateT dto, T obj);
    public void Delete (T obj);
}