
using DO;
namespace DalApi;
//System.Linq.Enumerable

public interface ICrud <T> where T : struct
{
    int Add(T item);
    T GetById(int id);
    void Update(T item);    
    void Delete(int id);

    IEnumerable<T?> GetAll(Fanc<T?,bool>? filtar=null);

}
