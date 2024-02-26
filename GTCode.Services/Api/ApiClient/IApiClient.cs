using GTCode.Services.Api.Response;
using GTCode.Services.Exceptions;

namespace GTCode.Services.Api.ApiClient
{
    /// <summary>
    /// Metodi HTTP volti alla comunicazione verso un server remoto.
    /// </summary>
    public interface IApiClient
    {

        /// <summary>
        /// Esegue una REQUEST-POST all'indirizzo fornito.
        /// </summary>        
        /// <typeparam name="TModel">oggetto in cui contenere la risposta</typeparam>
        /// <param name="url">inidirizzo a cui effetturare la richiesta</param>
        /// <param name="jsonObject">[optional] oggetto sa inserire nel body della richiesta</param>
        /// <param name="authenticationToken">[optional] token di autenticazione nel formato "username:password"</param>
        /// <exception cref="InternalException">se il corpo della risposta è NULL</exception>
        /// <returns>oggetto definito in TModel</returns>        
        Task<TModel> PostCallAPIAsync<TModel>(string url, object? jsonObject = null, string? authenticationToken = null) where TModel : GenericResponse;

        /// <summary>
        /// Esegue una REQUEST-POST all'indirizzo fornito.
        /// </summary>        
        /// <typeparam name="TModel">oggetto in cui contenere la risposta</typeparam>
        /// <param name="url">inidirizzo a cui effetturare la richiesta</param>
        /// <param name="parameters">parametri da inviare nella richiesta</param>
        /// <param name="authenticationToken">[optional] token di autenticazione nel formato "username:password"</param>
        /// <exception cref="InternalException">se il corpo della risposta è NULL</exception>
        /// <returns>oggetto definito in TModel</returns>
        Task<TModel> PostCallAPIAsync<TModel>(string url, Dictionary<string, string> parameters, string? authenticationToken = null) where TModel : GenericResponse;

        /// <summary>
        /// Carica sul server il file fornito all'indirizzo fornito in POST.
        /// </summary>
        /// <typeparam name="TModel">oggetto in cui contenere la risposta</typeparam>
        /// <param name="url">indirizzo a cui effetturare l'upload</param>
        /// <param name="file">byteArray rappresentante il contenuto del file da spedire</param>
        /// <param name="fileName">nome del file</param>
        /// <param name="fileToken">nome del parametro della request rappresentante il file</param>
        /// <param name="item">oggetto da spedire</param>
        /// <param name="authenticationToken">[optional] token di autenticazione nel formato "username:password"</param>
        /// <exception cref="InternalException">se il corpo della risposta è NULL.</exception>
        /// <returns>oggetto definito in TModel.</returns>
        Task<TModel> UploadAsync<TModel>(string url, byte[] file, string fileName, string fileToken, Dictionary<string, string> item, string? authenticationToken = null) where TModel : GenericResponse;

        /// <summary>
        /// Carica sul server i file indicati all'indirizzo fornito in POST.
        /// </summary>
        /// <typeparam name="TModel">oggetto in cui contenere la risposta</typeparam>
        /// <param name="url">indirizzo a cui effetturare l'upload</param>
        /// <param name="fileList">dictionary avente per 'key' il nome del parametro della requestest e per 'value' percorso del file da spedire</param>                
        /// <param name="item">oggetto da spedire</param>
        /// <param name="authenticationToken">[optional] token di autenticazione nel formato "username:password"</param>
        /// <exception cref="InternalException">se il corpo della risposta è NULL.</exception>
        /// <returns>oggetto definito in TModel.</returns>
        Task<TModel> UploadAsync<TModel>(string url, List<Tuple<string, string>> fileList, Dictionary<string, string> item, string? authenticationToken = null) where TModel : GenericResponse;

        /// <summary>
        /// Esegue una REQUEST-PUT all'indirizzo fornito.
        /// </summary>
        /// <typeparam name="TModel">oggetto in cui contenere la risposta</typeparam>
        /// <param name="url">inidirizzo a cui effetturare la richiesta</param>
        /// <param name="jsonObject">[optional] oggetto da inserire nel body della richiesta</param>
        /// <param name="authenticationToken">[optional] token di autenticazione nel formato "username:password"</param>
        /// <exception cref="InternalException">se il corpo della risposta è NULL</exception>
        /// <returns>oggetto definito in TModel</returns>
        Task<TModel> PutCallAPIAsync<TModel>(string url, object? jsonObject = null, string? authenticationToken = null) where TModel : GenericResponse;

        /// <summary>
        /// Esegue una REQUEST-PUT all'indirizzo fornito.
        /// </summary>
        /// <typeparam name="TModel">oggetto in cui contenere la risposta</typeparam>
        /// <param name="url">inidirizzo a cui effetturare la richiesta</param>
        /// <param name="parameters">parametri da inserire nell'header della richiesta</param>
        /// <param name="authenticationToken">[optional] token di autenticazione nel formato "username:password"</param>
        /// <exception cref="InternalException">se il corpo della risposta è NULL</exception>
        /// <returns>oggetto definito in TModel</returns>
        Task<TModel> PutCallAPIAsync<TModel>(string url, Dictionary<string, string> parameters, string? authenticationToken = null) where TModel : GenericResponse;

        /// <summary>
        /// Esegue una REQUEST-GET all'indirizzo fornito.
        /// </summary>
        /// <typeparam name="TModel">oggetto in cui contenenre la risposta.</typeparam>
        /// <param name="url">inidirizzo a cui effetturare la richiesta</param>
        /// <param name="authenticationToken">[optional] token di autenticazione nel formato "username:password"</param>
        /// <exception cref="InternalException">se il corpo della risposta è NULL</exception>
        /// <returns>oggetto definito in TModel.</returns>
        Task<TModel> GetCallAPIAsync<TModel>(string url, string? authenticationToken = null) where TModel : GenericResponse;

        /// <summary>
        /// Esegue una REQUEST-GET all'indirizzo fornito restituente un byte array.
        /// </summary>
        /// <param name="url">inidirizzo a cui effetturare la richiesta</param>
        /// <param name="authenticationToken">[optional] token di autenticazione nel formato "username:password"</param>
        /// <exception cref="InternalException">se il corpo della risposta è NULL</exception>
        /// <returns>byte array fornito nella risposta</returns>
        Task<byte[]> GetCallByteArrayAPIAsync(string url, string? authenticationToken = null);

        /// <summary>
        /// Esegue una REQUEST-GET all'indirizzo fornito restituente un byte array salvandolo nel percorso indicato.
        /// Il nome del file è deciso dal server.
        /// </summary>
        /// <param name="url">indirizzo a cui effettuare la richiesta</param>
        /// <param name="directory">percorso nel quale salvare il file</param>
        /// <param name="authenticationToken">[optional] token di autenticazione nel formato "username:password"</param>
        /// <exception cref="InternalException">se il corpo della risposta è NULL</exception>
        /// <returns>byte array fornito nella risposta</returns>
        Task DownloadFileAsync(string url, string directory, string? authenticationToken = null);

    }
}
