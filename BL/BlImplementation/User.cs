using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

internal class User : BlApi.IUser
{
    /// <summary>
    /// return all the users in the system
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.BlNullPropertyException"></exception>
    public IEnumerable<BO.User?> GetAllUsers()
    {
        return dal.User.GetAll().Select(user => new BO.User()
        {
            Name = user?.Name ?? throw new BO.BlNullPropertyException("missing user name"),
            Password = user?.Password ?? throw new BO.BlNullPropertyException("missing password"),
            LogIn = (BO.UserLogIn)(user?.LogIn)
        });
    }

    /// <summary>
    /// Updete a user details
    /// </summary>
    /// <param name="user"></param>
    /// <exception cref="BO.BlEmptyStringException"></exception>
    /// <exception cref="BO.BlMissingEntityException"></exception>
    public void UpdateUser(BO.User user)
    {
        if (user.Name == " ")
            throw new BO.BlEmptyStringException("user name");
        try
        {
            dal.User.Update(new DO.User() { Name = user.Name, Password = user.Password, LogIn = (DO.UserLogIn)(user.LogIn) });
        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("user doesnt exist", ex);
        }

    }
    /// <summary>
    /// return the users by enum
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<BO.User?> GetListedListByFilter(Func<BO.User?, bool>? filter = null)
    {
        return from BO.User u in GetAllUsers()
               where filter(u)
               select u;
    }
    /// <summary>
    /// delete a user
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="BO.BlMissingEntityException"></exception>
    public void DeleteUser(int id)
    {
        try
        {
            dal.User.Delete(id);//try to delete user with this id-password
        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("user doesnt exist", ex);
        }

    }
    DalApi.IDal dal = DalApi.Factory.Get();
    private delegate BO.User? sc<in T>(T obj);
    public delegate TOutput Converter<in TInput, out TOutput>(TInput input);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <exception cref="BO.BlMissingEntityException"></exception>
    public void Compare(BO.User user)
    {
        IEnumerable<DO.User> use = from DO.User u in dal.User.GetAll()
                                   where (u.Name == user.Name) && (u.Password == user.Password)/*&&((BO.LogIn)(u.Log)==user.Log)*//*&&(u.Email==user.Email)*/
                                   select u;
        if (use.Count() == 0)//if empty-there was nothing equals to the given user
            throw new BO.BlMissingEntityException("user doesnt exist");//user doesnt exist
        user.LogIn = (BO.UserLogIn)(use.First(u => (u.Name == user.Name) && (u.Password == user.Password)).LogIn);
        //exists
    }
    /// <summary>
    /// add a new user to the system
    /// </summary>
    /// <param name="user"></param>
    /// <exception cref="BO.BlEmptyStringException"></exception>
    /// <exception cref="BO.BlAlreadyExistEntityException"></exception>
    public void AddUser(BO.User user)
    {
        if (user.Name == " ")
            throw new BO.BlEmptyStringException("user name");
        try
        {
            dal.User.Add(new DO.User() { Name = user.Name, Password = user.Password, LogIn = (DO.UserLogIn)(user.LogIn) });
        }
        catch (DO.DalAlreadyExistIdException ex)
        {
            throw new BO.BlAlreadyExistEntityException(" this user already exists", ex);
        }
    }

}

