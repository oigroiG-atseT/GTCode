using GTCode.Services.Api.Response;
using GTCode.Services.Exceptions;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace GTCode.Services.Api.ApiClient
{
    /// <summary>
    /// Implementazione tramite HttpClient di IApiClient.
    /// </summary>
    public class ApiClient_HttpClient : IApiClient
    {
        /// <summary>
        /// HttpClient utilizzato per eseguire le richieste
        /// </summary>
        protected readonly HttpClient _httpClient;
        /// <summary>
        /// 
        /// </summary>
        private readonly Dictionary<string, AuthenticationHeaderValue?> _authCache = new();
        private readonly Func<string, string> _basicAuthEncodingFunction;

        /// <summary>
        /// Implementazione tramite HttpClient di IApiClient.
        /// </summary>
        public ApiClient_HttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _authCache.Add("default", httpClient.DefaultRequestHeaders.Authorization);
            _basicAuthEncodingFunction = (authenticationToken) => Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(authenticationToken));
        }

        /// <summary>
        /// Implementazione tramite HttpClient di IApiClient.
        /// </summary>
        public ApiClient_HttpClient(HttpClient httpClient, Func<string, string> basicAuthEncodingFunction) : this(httpClient)
        {
           _basicAuthEncodingFunction = basicAuthEncodingFunction;
        }

        public async Task<TModel> PostCallAPIAsync<TModel>(string url, object? jsonObject = null, string? authenticationToken = null) where TModel : GenericResponse
        {
            try
            {
                var content = new StringContent(String.Empty);
                if (jsonObject != null) content = new StringContent(JsonConvert.SerializeObject(jsonObject), Encoding.UTF8, "application/json");

                this.BaseAuthenticateCall(authenticationToken);
                using var response = await _httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                if (response is null) throw new InternalException(ExceptionsDefinition.API_NULL_BODY);
                
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TModel>(jsonString);                
            }
            finally { this.ResetAuthenticationCall(authenticationToken); }
        }

        public async Task<TModel> PostCallAPIAsync<TModel>(string url, Dictionary<string, string> parameters, string? authenticationToken) where TModel : GenericResponse
        {
            try
            {
                var encodedContent = new FormUrlEncodedContent(parameters);

                this.BaseAuthenticateCall(authenticationToken);
                using var response = await _httpClient.PostAsync(url, encodedContent).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                if (response is null) throw new InternalException(ExceptionsDefinition.API_NULL_BODY);

                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TModel>(jsonString);
            }
            finally { this.ResetAuthenticationCall(authenticationToken); }  
        }

        public async Task<TModel> UploadAsync<TModel>(string url, byte[] file, string fileName, string fileToken, Dictionary<string, string> item, string? authenticationToken) where TModel : GenericResponse
        {
            try
            {
                using var content = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                content.Add(new StreamContent(new MemoryStream(file)), fileToken, fileName);
                foreach (string key in item.Keys)
                {
                    content.Add(new StringContent(item[key]), key);
                }

                this.BaseAuthenticateCall(authenticationToken);
                using var responseMessage = await _httpClient.PostAsync(url, content);                
                if (responseMessage is null) throw new InternalException(ExceptionsDefinition.API_NULL_BODY);

                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TModel>(jsonString);
            } finally { this.ResetAuthenticationCall(authenticationToken); }                       
        }

        public async Task<TModel> UploadAsync<TModel>(string url, List<Tuple<string, string>> fileList, Dictionary<string, string> item, string? authenticationToken = null) where TModel : GenericResponse
        {
            try
            {
                using var content = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                
                foreach (var tuple in fileList)
                {
                    var file = new FileInfo(tuple.Item2);
                    content.Add(new StreamContent(new MemoryStream(File.ReadAllBytes(file.FullName))), tuple.Item1, file.Name);
                }
                                
                foreach (string key in item.Keys)
                {
                    content.Add(new StringContent(item[key]), key);
                }

                this.BaseAuthenticateCall(authenticationToken);
                using var responseMessage = await _httpClient.PostAsync(url, content);
                if (responseMessage is null) throw new InternalException(ExceptionsDefinition.API_NULL_BODY);

                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TModel>(jsonString);
            }
            finally { this.ResetAuthenticationCall(authenticationToken); }
        }

        public async Task<TModel> PutCallAPIAsync<TModel>(string url, object? jsonObject = null, string? authenticationToken = null) where TModel : GenericResponse
        {
            try
            {
                var content = new StringContent(String.Empty);
                if (jsonObject != null) content = new StringContent(JsonConvert.SerializeObject(jsonObject), Encoding.UTF8, "application/json");

                this.BaseAuthenticateCall(authenticationToken);
                using var response = await _httpClient.PutAsync(url, content);
                response.EnsureSuccessStatusCode();
                if (response is null) throw new InternalException(ExceptionsDefinition.API_NULL_BODY);

                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TModel>(jsonString);
            }
            finally { this.ResetAuthenticationCall(authenticationToken); }            
        }

        public async Task<TModel> PutCallAPIAsync<TModel>(string url, Dictionary<string, string> parameters, string? authenticationToken = null) where TModel : GenericResponse
        {
            try
            {
                var encodedContent = new FormUrlEncodedContent(parameters);

                this.BaseAuthenticateCall(authenticationToken);
                using var response = await _httpClient.PutAsync(url, encodedContent).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                if (response is null) throw new InternalException(ExceptionsDefinition.API_NULL_BODY);
                
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TModel>(jsonString);
            }
            finally { this.ResetAuthenticationCall(authenticationToken); }
        }

        public async Task<TModel> GetCallAPIAsync<TModel>(string url, string? authenticationToken = null) where TModel : GenericResponse
        {
            try
            {
                this.BaseAuthenticateCall(authenticationToken);
                using var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                if (response is null) throw new InternalException(ExceptionsDefinition.API_NULL_BODY);
                
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TModel>(jsonString);
            }
            finally { this.ResetAuthenticationCall(authenticationToken); }
        }

        public async Task<byte[]> GetCallByteArrayAPIAsync(string url, string? authenticationToken = null)
        {
            try
            {
                this.BaseAuthenticateCall(authenticationToken);
                var response = await _httpClient.GetByteArrayAsync(url);                
                if (response is null) throw new InternalException(ExceptionsDefinition.API_NULL_BODY);
                return response;
            }
            finally { this.ResetAuthenticationCall(authenticationToken); }
        }

        public async Task DownloadFileAsync(string url, string directory, string? authenticationToken = null)
        {            
            try
            {
                this.BaseAuthenticateCall(authenticationToken);
                
                var message = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await _httpClient.SendAsync(message, HttpCompletionOption.ResponseHeadersRead);

                if (response.IsSuccessStatusCode)
                {
                    var fileName = response.Content.Headers.ContentDisposition.FileName;
                    using var stream = await response.Content.ReadAsStreamAsync();
                    using var fileStream = File.Create(@$"{directory}\{fileName.Replace("\"", string.Empty)}");
                    await stream.CopyToAsync(fileStream);
                }
            }            
            finally { this.ResetAuthenticationCall(authenticationToken); }
        }

        public async Task DownloadFileAsync(string url, string directory, object? jsonObject = null, string? authenticationToken = null)
        {
            try
            {
                this.BaseAuthenticateCall(authenticationToken);

                var message = new HttpRequestMessage(HttpMethod.Post, url);
                if(jsonObject != null) 
                    message.Content = new StringContent(JsonConvert.SerializeObject(jsonObject), Encoding.UTF8, "application/json");
                var response = await _httpClient.SendAsync(message, HttpCompletionOption.ResponseHeadersRead);

                if (response.IsSuccessStatusCode)
                {
                    var fileName = response.Content.Headers.ContentDisposition.FileName;
                    using var stream = await response.Content.ReadAsStreamAsync();
                    using var fileStream = File.Create(@$"{directory}\{fileName.Replace("\"", string.Empty)}");
                    await stream.CopyToAsync(fileStream);
                }
            }
            finally { this.ResetAuthenticationCall(authenticationToken); }
        }

        #region METHODS   

        /// <summary>
        /// Autentica la chiamata verso il server utilizzando il token fornito.
        /// </summary>
        /// <param name="authenticationToken">token di autenticazione della richiesta, nel formato username:password</param>
        /// <exception cref="ArgumentException">se il token non rispetta i formalismi</exception>
        protected void BaseAuthenticateCall(string? authenticationToken)
        {
            if (authenticationToken is null) return;            
            if (!Regex.Match(authenticationToken, "(.*):(.*)").Success) throw new ArgumentException();
            if (!_authCache.Any(auth => auth.Key.Equals(authenticationToken)))
            {
                string encodedToken = _basicAuthEncodingFunction.Invoke(authenticationToken);
                _authCache.Add(authenticationToken, new AuthenticationHeaderValue("Basic", encodedToken));                
            }
            _httpClient.DefaultRequestHeaders.Authorization = _authCache[authenticationToken];
        }

        /// <summary>
        /// Ripristina gli headers dell'httpClient.
        /// </summary>
        /// <param name="authenticationToken">token di autenticazione</param>
        protected void ResetAuthenticationCall(string? authenticationToken)
        {
            if (authenticationToken is null) return;            
            _httpClient.DefaultRequestHeaders.Authorization = _authCache["default"];
        }

        #endregion METHODS

    }
}
