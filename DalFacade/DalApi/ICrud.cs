//using System.Linq.Enumerable;

using DO;
namespace DalApi;

public interface ICrud <T> where T : struct
{
    /// <summary>
    /// add
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    int Add(T item);
    /// <summary>
    /// get by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    T GetById(int id);
    /// <summary>
    /// update
    /// </summary>
    /// <param name="item"></param>
    void Update(T item);    
    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="id"></param>
    void Delete(int id);
    /// <summary>
    /// Get all the items
    /// </summary>
    /// <param name="filtar"></param>
    /// <returns></returns>
    public IEnumerable<T?> GetAll(Func<T?,bool>? filtar=null);

}
