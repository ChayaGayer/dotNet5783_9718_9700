using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class User
{
    /// <summary>
    /// Name 
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// email
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// maneger or coustomer enum
    /// </summary>
    public UserLogIn LogIn { get; set; }
    /// <summary>
    /// the Password to get into the system
    /// </summary>
    public int Password { get; set; }


}