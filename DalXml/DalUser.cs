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
    readonly string s_users = "users";
    /// <summary>
    /// user in exelemnt method
    /// </summary>
    /// <param name="u"></param>
    /// <returns></returns>
    static DO.User? createUserfromXElement(XElement u)
    {
        
        DO.User user = new DO.User()
        {
            Name = (string?)u.Element("Name") ?? "",
            Email = (string?)u.Element("Email") ?? "",
            LogIn = u.ToEnumNullable<DO.UserLogIn>("LogIn") ?? DO.UserLogIn.Coustomer,
            Password = u.ToIntNullable("Password") ?? 0
        };
        return user;
    }
    /// <summary>
    /// add user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    /// <exception cref="DO.DalAlreadyExistIdException"></exception>
    public int Add(User user)
    {
        XElement? usersRootElem = XMLTools.LoadListFromXMLElement(s_users);
        XElement? us = (from u in usersRootElem.Elements()
                        where (string?)u.Element("Name") == user.Name
                        select u).FirstOrDefault();
        if (us != null)
            throw new DO.DalAlreadyExistIdException(user.Password, "the user already exist"); 

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
    /// <summary>
    /// delete a user
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        XElement? usersRootElem = XMLTools.LoadListFromXMLElement(s_users);
        XElement? us = (from st in usersRootElem.Elements()
                        where (int?)st.Element("ID") == id
                        select st).FirstOrDefault() ?? throw new DO.DalMissingIdException(id,"missing id"); // fix to: throw new DalMissingIdException(id);

        us.Remove(); 

        XMLTools.SaveListToXMLElement(usersRootElem, s_users);
    }
    /// <summary>
    /// Get all the users
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Get user by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DO.DalMissingIdException"></exception>
    public User GetById(int id)
    {
        XElement? usersRootElem = XMLTools.LoadListFromXMLElement(s_users);
        return (from u in usersRootElem?.Elements()
                where u.ToIntNullable("Password") == id
                select (DO.User?)createUserfromXElement(u)).FirstOrDefault()
               ?? throw new DO.DalMissingIdException(id, "the passowrd isnt correct"); 
    }
    /// <summary>
    /// Update
    /// </summary>
    /// <param name="user"></param>
    public void Update(User user)
    {
        Delete(user.Password);
        Add(user);
    }
 
}
