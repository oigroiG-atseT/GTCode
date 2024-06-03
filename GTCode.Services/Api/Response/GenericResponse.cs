using GTCode.Services.Exceptions;

namespace GTCode.Services.Api.Response
{
    /// <summary>
    /// Risposta generica restituita da un server.
    /// </summary>
    public class GenericResponse
    {

        public bool? Success { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// In caso di errore solleva una "ServerException" valorizzata in base alla configurazione fornita.
        /// </summary>
        /// <param name="options">
        /// (OPTIONAL) impostazioni che descrivono il comportamento del metodo. Se null viene utilizata la configurazione di default.<br/>
        /// Le configurazioni attualmente utilizzate da questo metodo sono "THROW_SERVER_EXCEPTION_ACTION".
        /// </param>
        /// <exception cref="ServerException">se Success è false e Message contiene un valore</exception>
        public void CheckStatus(ResponseOptions options = null)
        {
            if (Success.HasValue && Success.Value) return;            
            options ??= ResponseOptions.DEFAULT;
            options.THROW_SERVER_EXCEPTION_ACTION.Invoke(Message);                            
        }

    }
}
