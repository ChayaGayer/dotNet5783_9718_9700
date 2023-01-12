using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal class DalUser : IUser
{
    const string s_users = "users";
    static DO.User? createUserfromXElement(XElement u)
    {
        DO.UserLogIn userLogIn;
        Enum.TryParse((string?)u.Element("UserLogIn"), out userLogIn);
        return new DO.User()
        {
            Name=(string?)u.Element("Name"),
            Email=(string?)u.Element("Email"),
            LogIn=userLogIn,
            Password=(int)u.ToIntNullable("Password")           
        };
    }

    public int Add(User user)
    {
        XElement? usersRootElem = XMLTools.LoadListFromXMLElement(s_users);
        XElement? us = (from u in usersRootElem.Elements()
                          where (string?)u.Element("Name") == user.Name //where (int?)st.Element("ID") == doStudent.ID
                          select u).FirstOrDefault();
        if (us != null)
            throw new Exception("the user already exist"); // fix to: throw new DalMissingIdException(id);

        XElement userElem = new XElement("User",
                                   new XElement("Name", user.Name),
                                   new XElement("Email", user.Name),
                                   new XElement("LogIn", user.LogIn),
                                   new XElement("Password", user.Password)
                                  
                                   );

        usersRootElem.Add(userElem);

        XMLTools.SaveListToXMLElement(usersRootElem, s_users);

        return user.Password; ;
    }

    public void Delete(int id)
    {
        XElement? usersRootElem = XMLTools.LoadListFromXMLElement(s_users);
        XElement? us = (from st in usersRootElem.Elements()
                          where (int?)st.Element("ID") == id
                          select st).FirstOrDefault() ?? throw new Exception("missing id"); // fix to: throw new DalMissingIdException(id);

        us.Remove(); //<==>   Remove stud from studentsRootElem

        XMLTools.SaveListToXMLElement(usersRootElem, s_users);
    }

    public IEnumerable<User?> GetAll(Func<User?, bool>? filter = null)
    {
        XElement? usersRootElem = XMLTools.LoadListFromXMLElement(s_users);
        if (filter != null)
        {
            return from u in usersRootElem.Elements()
                   let user = createUserfromXElement(u)
                   where filter(user)
                   select (DO.User?)user;
        }
        else
        {
            return from s in usersRootElem.Elements()
                   select createUserfromXElement(s);
        }

    }

    public User GetById(int id)
    {
        XElement? usersRootElem = XMLTools.LoadListFromXMLElement(s_users);
        //return DataSource.UsersList.FirstOrDefault(user => user?.Password == id) ?? throw new DalAlreadyExistIdException(id, "user");
        return (from u in usersRootElem?.Elements()
                where u.ToIntNullable("Password") == id
                select (DO.User?)createUserfromXElement(u)).FirstOrDefault()
               ?? throw new DO.DalMissingIdException(id,"the passowrd isnt correct"); // fix to: throw new DalMissingIdException(id);
    }

    public void Update(User user)
    {
        Delete(user.Password);
        Add(user);
    }
    //readonly string s_users = "users";
    //public int Add(User user)
    //{
    //    List<DO.User?> listUsers = XMLTools.LoadListFromXMLSerializer<DO.User>(s_users);
    //    bool flag = listUsers.Any(use => use?.Password == user.Password);
    //    if (flag)//if this password already exist-cant add this user
    //        throw new DO.DalAlreadyExistIdException(user.Password, "user");
    //    //the new user doesnt exist
    //    listUsers.Add(user);
    //    XMLTools.SaveListToXMLSerializer(listUsers, s_users);
    //    return user.Password;
    //}

    //public void Delete(int id)
    //{
    //    List<DO.User?> listUsers = XMLTools.LoadListFromXMLSerializer<DO.User>(s_users);
    //    User? adduser = listUsers.FirstOrDefault(user => user?.Password == id) ?? throw new DalAlreadyExistIdException(id, "user");
    //    int UserIndex = listUsers.FindIndex(user => user?.Password == id);
    //    listUsers.RemoveAt(UserIndex);
    //    XMLTools.SaveListToXMLSerializer(listUsers, s_users);
    //}

    //public IEnumerable<User?> GetAll(Func<User?, bool>? filtar = null)
    //{
    //    List<DO.User?> listUsers = XMLTools.LoadListFromXMLSerializer<DO.User>(s_users);
    //    if (filtar != null)
    //        return listUsers.Where(item => filtar(item));
    //    return listUsers.Select(item => item);
    //}

    //public User GetById(int id)
    //{
    //    List<DO.User?> listUsers = XMLTools.LoadListFromXMLSerializer<DO.User>(s_users);
    //    return listUsers.FirstOrDefault(user => user?.Password == id) ?? throw new DalAlreadyExistIdException(id, "user");
    //}

    //public void Update(User user)
    //{
    //    List<DO.User?> listUsers = XMLTools.LoadListFromXMLSerializer<DO.User>(s_users);
    //    User? addUser = listUsers.FirstOrDefault(user => user?.Password == user?.Password) ?? throw new DalAlreadyExistIdException(user.Password, "user");
    //    int UserIndex = listUsers.FindIndex(user => user?.Password == user?.Password);
    //    listUsers[UserIndex] = user;
    //    XMLTools.SaveListToXMLSerializer(listUsers, s_users);

    //}
}
