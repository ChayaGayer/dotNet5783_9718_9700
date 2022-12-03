using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{/// <summary>
/// exception that thrown when the Entity already exist
/// </summary>
    [Serializable]
    public class DalAlreadyExistIdException : Exception
    {
        public int EntityID;
        public string EntityName;
        public DalAlreadyExistIdException(int id, string name) : base()
        {
            EntityID = id;
            EntityName = name;
        }
        public DalAlreadyExistIdException(int id, string name, string message) : base(message)
        {
            EntityID = id;
            EntityName = name;
        }
        public DalAlreadyExistIdException(int id, string name, string message, Exception innerException) : base(message, innerException)
        {
            EntityID = id;
            EntityName = name;
        }
        public override string ToString() => $"Id:{EntityID}ofType {EntityName},is already exist.";


    }
    /// <summary>
    /// exception that thrown when the Entity is missing 
    /// </summary>
    [Serializable]
    public class DalMissingIdException : Exception
    {
        public int EntityID;
        public string EntityName;
        public DalMissingIdException(int id, string name) : base()
        {
            EntityID = id;
            EntityName = name;
        }
        public DalMissingIdException(int id, string name, string message) : base(message)
        {
            EntityID = id;
            EntityName = name;
        }
        public DalMissingIdException(int id, string name, string message, Exception innerException) : base(message, innerException)
        {
            EntityID = id;
            EntityName = name;
        }
        public override string ToString() => $"Id:{EntityID}ofType {EntityName},is missing.";


    }
  
}