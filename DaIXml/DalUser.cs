using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class DalUser : IUser
{
    readonly string s_users = "users";
    public int Add(User user)
    {
        List<DO.User?> listUsers= XMLTools.LoadListFromXMLSerializer<DO.User>(s_users);
        bool flag = listUsers.Any(use => use?.Password == user.Password);
        if (flag)//if this password already exist-cant add this user
            throw new DO.DalAlreadyExistIdException(user.Password, "user");
        //the new user doesnt exist
        listUsers.Add(user);
        XMLTools.SaveListToXMLSerializer(listUsers, s_users);
        return user.Password;
    }

    public void Delete(int id)
    {
        List<DO.User?> listUsers = XMLTools.LoadListFromXMLSerializer<DO.User>(s_users);
        User? adduser = listUsers.FirstOrDefault(user => user?.Password == id) ?? throw new DalAlreadyExistIdException(id, "user");
        int UserIndex = listUsers.FindIndex(user => user?.Password == id);
        listUsers.RemoveAt(UserIndex);
        XMLTools.SaveListToXMLSerializer(listUsers, s_users);
    }

    public IEnumerable<User?> GetAll(Func<User?, bool>? filtar = null)
    {
        List<DO.User?> listUsers = XMLTools.LoadListFromXMLSerializer<DO.User>(s_users);
        if (filtar != null)
            return listUsers.Where(item => filtar(item));
        return listUsers.Select(item => item);
    }

    public User GetById(int id)
    {
        List<DO.User?> listUsers = XMLTools.LoadListFromXMLSerializer<DO.User>(s_users);
        return listUsers.FirstOrDefault(user => user?.Password == id) ?? throw new DalAlreadyExistIdException(id, "user");
    }

    public void Update(User user)
    {
        List<DO.User?> listUsers = XMLTools.LoadListFromXMLSerializer<DO.User>(s_users);
        User? addUser = listUsers.FirstOrDefault(user => user?.Password == user?.Password) ?? throw new DalAlreadyExistIdException(user.Password, "user");
        int UserIndex = listUsers.FindIndex(user => user?.Password == user?.Password);
        listUsers[UserIndex] = user;
        XMLTools.SaveListToXMLSerializer(listUsers, s_users);
        
    }
}
}
