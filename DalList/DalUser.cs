using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

public class DalUser : IUser
{
    /// <summary>
    /// add new user into the system and return his password
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public int Add(User user)
    {
        bool flag = DataSource.UsersList.Any(use => use?.Password == user.Password);
        if (flag)//if this password already exist-cant add this user
            throw new DO.DalAlreadyExistIdException(user.Password, "user");
        //the new user doesnt exist
        DataSource.UsersList.Add(user);
        return user.Password;
    }
    public void Delete(int id)//for now-dont delete any user...
    {
        User? adduser = DataSource.UsersList.FirstOrDefault(user => user?.Password == id) ?? throw new DalAlreadyExistIdException(id, "user");
        int UserIndex = DataSource.UsersList.FindIndex(user => user?.Password == id);
        DataSource.UsersList.RemoveAt(UserIndex);
    }

    public IEnumerable<User?> GetAll(Func<User?, bool>? filter = null)
    {
        if (filter != null)
            return DataSource.UsersList.Where(item => filter(item));
        return DataSource.UsersList.Select(item => item);
    }

    public User GetById(int id)
    {
        return DataSource.UsersList.FirstOrDefault(user => user?.Password == id) ?? throw new DalAlreadyExistIdException(id, "user");

    }

    public void Update(User user)
    {
        User? addUser = DataSource.UsersList.FirstOrDefault(user => user?.Password == user?.Password) ?? throw new DalAlreadyExistIdException(user.Password, "user");
        int UserIndex = DataSource.UsersList.FindIndex(user => user?.Password == user?.Password);
        DataSource.UsersList[UserIndex] = user;
    }
}
