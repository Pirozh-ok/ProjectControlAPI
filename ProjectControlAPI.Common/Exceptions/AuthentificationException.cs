namespace ProjectControlAPI.Common.Exceptions
{
    public class AuthentificationException : Exception
    {
        public int ErrorCode { get; set; } = 401;
        public AuthentificationException(string message)
            : base(message)
        {
        }
    }
}
