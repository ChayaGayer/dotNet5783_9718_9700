
namespace BO
{
    /// <summary>
    /// if the entity allready exist throw an exception
    /// </summary>
    [Serializable]
    public class BlAlreadyExistEntityException : Exception 
    {
        public BlAlreadyExistEntityException(string message,Exception innerException):base(message, innerException)
        {

        }
        public override string ToString() => base.ToString() + $"Entity is already exist.";
       
    }

    /// <summary>
    /// if the entity is missing throw an exception
    /// </summary>
    [Serializable]
    public class BlMissingEntityException:Exception
    {
        public BlMissingEntityException(string message,Exception innerException):base(message,innerException)
        {

        }
        public BlMissingEntityException(string message) : base(message)
        {

        }
        public override string ToString() => base.ToString + $"Missing Entity";
        
    }

    /// <summary>
    /// if the Property null throw an exception
    /// </summary>
    [Serializable]
    public class BlNullPropertyException:Exception
    {
        public  BlNullPropertyException(string message):base(message)
        {

        }
    }
    /// <summary>
    /// if its not the category throw an exception
    /// </summary>
    [Serializable]
    public class BlWorngCategoryException : Exception
    {
        public BlWorngCategoryException(string message) : base(message)
        {

        }
    }
    /// <summary>
    /// if the date in correct throw an exception
    /// </summary>
    [Serializable]
    public class BlIncorrectDatesException : Exception
    {
        public BlIncorrectDatesException(string message) : base(message)
        {

        }
    }
    /// <summary>
    /// if  its Nagtive throw an exception
    /// </summary>
    [Serializable]
    public class BlNagtiveNumberException : Exception
    {
        public BlNagtiveNumberException(string message) : base(message)
        {

        }

    }
    /// <summary>
    /// if there is an empty string throw an exception
    /// </summary>

    [Serializable]
    public class BlEmptyStringException : Exception
    {
        public BlEmptyStringException(string? message) : base(message)
        {

        }
    }
    /// <summary>
    /// if its in correct throw an exception
    /// </summary>
    [Serializable]
    public class BlInCorrectException : Exception
    {
        public BlInCorrectException(string? message) : base(message)
        {

        }
    }

   
}
