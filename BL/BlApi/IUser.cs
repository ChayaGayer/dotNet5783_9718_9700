using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IUser
{
    public void AddUser(BO.User u, int LogIn);
    public BO.User GetByUserPasswprd(string name, int password);
    //public IEnumerable<BO.User?> GetAllUsers();
    //public void UpdateUser(BO.User user);
    //public IEnumerable<BO.User?> GetListedListByFilter(Func<BO.User?, bool>? filter = null);
    //public void DeleteUser(int id);
    //public void Compare(BO.User user);
    //public void AddUser(BO.User user);
}
