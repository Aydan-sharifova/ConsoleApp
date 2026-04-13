namespace RepositoryLayer.Exceptions
{
    public class StudentNameRequiredException : Exception
    {
        public StudentNameRequiredException(string message) : base(message)
        {
        }
    }
}
