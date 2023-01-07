using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

public struct User
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
    /// maneger or coustomer
    /// </summary>
    public UserLogIn LogIn { get; set; }
    /// <summary>
    /// the Password to get into the system
    /// </summary>
    public int Password { get; set; }


}
