using GTCode.Services.Exceptions;

namespace GTCode.Services.Api.Response
{
    /// <summary>
    /// Insieme di impostazioni che descrivono il comporamento di vari metodi relativi alla gestione delle "Response".
    /// </summary>
    public class ResponseOptions
    {

        #region DEFINITIONS
        internal Action<string> ALWAYS_THROW_AS_SERVER_EXCEPTION_ACTION = (message) => throw new ServerException(ExceptionsDefinition.API_SERVER_EXCEPTION + message);
        internal Action<string> BASE_THROW_AS_SERVER_EXCEPTION_ACTION = (message) => throw new ServerException(string.IsNullOrEmpty(message) ? ExceptionsDefinition.API_SERVER_EXCEPTION.Replace(":\n", "") : message);

        internal Action<string, Action<string>> ONLY_THROW_ON_MESSAGE_ACTION = (message, throwAction) => { if (!string.IsNullOrEmpty(message)) throwAction.Invoke(message); };
        internal Action<string, Action<string>> BASE_THROW_ON_MESSAGE_ACTION = (message, throwAction) => { throwAction.Invoke(message); };
        #endregion DEFINITIONS

        /// <summary>
        /// Indica alle implementazioni di "CheckStatus" di gestire qualsiasi messaggio restituito dal server come errore.<br/>
        /// Se false le implementazioni di "CheckStatus" sollevano una "ServerException" con errore di default quando Response.Message è vuoto,
        /// altrimenti una "ServerException" con errore il messaggio restituito dal server.
        /// </summary>
        public bool ALWAYS_THROW_AS_SERVER_EXCEPTION;
        /// <summary>
        /// Indica alle implementazioni di "CheckStatus" di sollevare un eccezione solo se "Response.Message" è valorizzato.
        /// </summary>
        public bool ONLY_THROW_ON_MESSAGE;

        /// <summary>
        /// Configurazione di default. Sovrascrivere questa variabile modifica il coportamento di tutti i metodi ai quali non è stato
        /// fornito un "ResponseOptions" specifico.
        /// </summary>
        public static ResponseOptions DEFAULT { get; set; } = new ResponseOptions();

        /// <summary>
        /// Crea un nuovo "ResponseOptions" con i seguenti attributi inizializzati:<br/>
        /// "ALWAYS_THROW_AS_SERVER_EXCEPTION" => TRUE
        /// "ONLY_THROW_ON_MESSAGE" => TRUE
        /// </summary>
        public ResponseOptions()
        {
            ALWAYS_THROW_AS_SERVER_EXCEPTION = true;
            ONLY_THROW_ON_MESSAGE = true;
        }

        #region INTERNALS
        internal Action<string> THROW_EXCEPTION_ACTION => ALWAYS_THROW_AS_SERVER_EXCEPTION ? ALWAYS_THROW_AS_SERVER_EXCEPTION_ACTION : BASE_THROW_AS_SERVER_EXCEPTION_ACTION;
        internal Action<string> THROW_SERVER_EXCEPTION_ACTION => (message) =>
        {
            if (ONLY_THROW_ON_MESSAGE)
                ONLY_THROW_ON_MESSAGE_ACTION.Invoke(message, THROW_EXCEPTION_ACTION);
            else
                BASE_THROW_ON_MESSAGE_ACTION.Invoke(message, THROW_EXCEPTION_ACTION);
        };
            
        #endregion INTERNALS

    }
}
