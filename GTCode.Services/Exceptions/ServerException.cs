namespace GTCode.Services.Exceptions
{
    /// <summary>
    /// Rappresenta un eccezione sollevata da un server remoto contattato tramite IApiClient/>
    /// </summary>
    [Serializable]
    public class ServerException: Exception
    {

        public ServerException() : base() { }
        public ServerException(string message) : base(message) { }
        public ServerException(string message, Exception inner) : base(message, inner) { }

        protected ServerException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context
        ) : base(info, context) { }

    }
}
