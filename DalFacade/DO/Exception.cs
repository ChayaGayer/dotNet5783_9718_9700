using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [Serializable]
    public class NagtiveNumberException : Exception
    {
        public NagtiveNumberException(string message) : base(message)
        {

        }

    }
    [Serializable]
    public class EmptyString : Exception
    {
        public EmptyString(string? message) : base(message)
        {

        }
    }
    [Serializable]
    public class NotExist : Exception
    {
        public NotExist(string? message) : base(message)
        {

        }
        public NotExist(Exception innerException, string? message = " ") : base(message, innerException)
        {

        }
    }
    [Serializable]
    public class AlredyExist : Exception
    {
        public AlredyExist(string? message) : base(message)
        {

        }
        public AlredyExist(Exception innerException, string? message = " ") : base(message, innerException)
        {

        }

    }
}
