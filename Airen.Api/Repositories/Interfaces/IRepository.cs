namespace Airen.Api.Repositories;

public interface IRepository<T, ReadT>
{
    public IEnumerable<ReadT> GetAll();
    public T? GetById(int id);
}