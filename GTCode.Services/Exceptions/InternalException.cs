namespace GTCode.Services.Exceptions
{
    /// <summary>
    /// Rappresenta un eccezione interna alla libreria.
    /// </summary>
    [Serializable]
    public class InternalException : Exception
    {

        public InternalException() : base() { }
        public InternalException(string message) : base(message) { }
        public InternalException(string message, Exception inner) : base(message, inner) { }

    }
}
