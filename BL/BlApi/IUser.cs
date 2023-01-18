using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IUser
{
   /// <summary>
   /// func that return a the users
   /// </summary>
   /// <returns></returns>
    public IEnumerable<BO.User?> GetAllUsers();
    /// <summary>
    /// get user and update him
    /// </summary>
    /// <param name="user"></param>
    public void UpdateUser(BO.User user);
    /// <summary>
    /// return the lust of product by filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<BO.User?> GetListedListByFilter(Func<BO.User?, bool>? filter = null);
    /// <summary>
    /// get id and return this users
    /// </summary>
    /// <param name="id"></param>
    public void DeleteUser(int id);
    /// <summary>
    /// check if not repet of user,only one user with the passward
    /// </summary>
    /// <param name="user"></param>
    public void Checking(BO.User user);
    /// <summary>
    /// get user and add it to the list
    /// </summary>
    /// <param name="user"></param>
    public void AddUser(BO.User user);
}
