using GTCode.Services.Exceptions;

namespace GTCode.Services.Api.Response
{
    /// <summary>
    /// Risposta generica restituita da un server.
    /// </summary>
    public class GenericResponse
    {

        public bool? Success { get; set; }

        public string? Message { get; set; }

        /// <summary>
        /// Se Success è false e Message contiene un valore solleva una ServerException
        /// contenente Message.
        /// </summary>
        /// <exception cref="ServerException">se Success è false e Message contiene un valore</exception>
        public void CheckStatus()
        {
            if ((Success.HasValue && !Success.Value) && !String.IsNullOrEmpty(Message))
                throw new ServerException(ExceptionsDefinition.API_SERVER_EXCEPTION + Message);            
        }

    }
}
