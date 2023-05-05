using System.Collections.Generic;

namespace AudioLibrary.Repositories.Abstract;

public interface IRepository<T>
{
    public IEnumerable<T> GetAll();
    public void SaveAll(IEnumerable<T> items);
}
