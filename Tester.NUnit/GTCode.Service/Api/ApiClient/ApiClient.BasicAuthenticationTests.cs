using GTCode.Services.Api.ApiClient;
using GTCode.Services.Api.Response;

namespace Tester.NUnit.GTCode.Service.Api.ApiClient
{
    [TestFixture]
    public class ApiClientBasicAuthenticationTests
    {

        private readonly IApiClient _apiClient = new ApiClient_HttpClient(new HttpClient());

        private readonly string API_BASE_URL = "http://localhost:3002/gt-code/basic-auth";

        private readonly string AUTH_TOKEN = "gtcode:gtcode";
        private readonly string UNAUTH_TOKEN = "code:code";

        [Category("Api")]
        [Category("GET")]
        [Order(0), Test(Description = "Verifica la corretta esecuzione di GetCallAPIAsync")]
        public void Test000_GetCallAPIAsync()
        {
            string url = $"{API_BASE_URL}/get-people";

            var result = _apiClient.GetCallAPIAsync<ListResponse<string>>(url, AUTH_TOKEN).Result;

            int size = 10;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Success, Is.True);
            Assert.That(result.TotalCount, Is.EqualTo(size));
            Assert.That(result.Data, Has.Count.EqualTo(size));
        }

        [Category("Api")]
        [Category("GET")]
        [Order(1), Test(Description = "Verifica la corretta esecuzione di GetCallByteArrayAPIAsync")]
        public void Test001_GetCallByteArrayAPIAsync()
        {
            string url = $"{API_BASE_URL}/get-file";

            var result = _apiClient.GetCallByteArrayAPIAsync(url, AUTH_TOKEN).Result;

            int size = 29;
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Length.AtLeast(size));
        }

        [Category("Api")]
        [Category("POST")]
        [Order(2), Test(Description = "Verifica la corretta esecuzione di PostCallAPIAsync")]
        public void Test002_PostCallAPIAsync()
        {
            string url = $"{API_BASE_URL}/post-mirror";
            var singleWrapper = new SingleResponse<string>()
            {
                Success = false,
                Message = "body restituito",
                Data = "PostCallAPIAsync"
            };

            var result = _apiClient.PostCallAPIAsync<SingleResponse<string>>(url, singleWrapper, AUTH_TOKEN).Result;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ToString(), Is.EqualTo(singleWrapper.ToString()));
        }

        [Category("Api")]
        [Category("PUT")]
        [Order(3), Test(Description = "Verifica la corretta esecuzione di PutCallAPIAsync(url, object)")]
        public void Test003_PutCallAPIAsync_object()
        {
            string url = $"{API_BASE_URL}/put-mirror";
            var singleWrapper = new SingleResponse<string>()
            {
                Success = false,
                Message = "body restituito",
                Data = "PutCallAPIAsync"
            };

            var result = _apiClient.PutCallAPIAsync<SingleResponse<string>>(url, singleWrapper, AUTH_TOKEN).Result;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ToString(), Is.EqualTo(singleWrapper.ToString()));
        }

        [Category("Api")]
        [Category("PUT")]
        [Order(4), Test(Description = "Verifica la corretta esecuzione di PutCallAPIAsync(url, Dictionary)")]
        public void Test004_PutCallAPIAsync_dictionary()
        {
            string url = $"{API_BASE_URL}/put-parameters-mirror";
            var dictionary = new Dictionary<string, string>()
            {
                {"1", "uno"}, {"2", "due"}, {"3", "tre"}
            };

            var result = _apiClient.PutCallAPIAsync<SingleResponse<string>>(url, dictionary, AUTH_TOKEN).Result;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Data, Is.EqualTo("uno"));
        }

        [Category("Api")]
        [Category("GET")]
        [Order(5), Test(Description = "Verifica la corretta esecuzione di GetCallAPIAsync")]
        public void Test005_GetCallAPIAsync_unauthorized()
        {
            string url = $"{API_BASE_URL}/get-people";

            Assert.ThrowsAsync<HttpRequestException>(async () => { await _apiClient.GetCallAPIAsync<ListResponse<string>>(url, UNAUTH_TOKEN); });            
        }

        [Category("Api")]
        [Category("GET")]
        [Order(6), Test(Description = "Verifica la corretta esecuzione di GetCallByteArrayAPIAsync")]
        public void Test006_GetCallByteArrayAPIAsync_unauthorized()
        {
            string url = $"{API_BASE_URL}/get-file";

            Assert.ThrowsAsync<HttpRequestException>(async () => { await _apiClient.GetCallByteArrayAPIAsync(url, UNAUTH_TOKEN); });            
        }

        [Category("Api")]
        [Category("POST")]
        [Order(7), Test(Description = "Verifica la corretta esecuzione di PostCallAPIAsync")]
        public void Test007_PostCallAPIAsync_unauthorized()
        {
            string url = $"{API_BASE_URL}/post-mirror";
            var singleWrapper = new SingleResponse<string>()
            {
                Success = false,
                Message = "body restituito",
                Data = "PostCallAPIAsync"
            };
            
            Assert.ThrowsAsync<HttpRequestException>(async () => { await _apiClient.PostCallAPIAsync<SingleResponse<string>>(url, singleWrapper, UNAUTH_TOKEN); });
        }

        [Category("Api")]
        [Category("PUT")]
        [Order(8), Test(Description = "Verifica la corretta esecuzione di PutCallAPIAsync(url, object)")]
        public void Test008_PutCallAPIAsync_object_unauthorized()
        {
            string url = $"{API_BASE_URL}/put-mirror";
            var singleWrapper = new SingleResponse<string>()
            {
                Success = false,
                Message = "body restituito",
                Data = "PutCallAPIAsync"
            };
            
            Assert.ThrowsAsync<HttpRequestException>(async () => { await _apiClient.PutCallAPIAsync<SingleResponse<string>>(url, singleWrapper, UNAUTH_TOKEN); });
        }

        [Category("Api")]
        [Category("PUT")]
        [Order(9), Test(Description = "Verifica la corretta esecuzione di PutCallAPIAsync(url, Dictionary)")]
        public void Test009_PutCallAPIAsync_dictionary_unauthorized()
        {
            string url = $"{API_BASE_URL}/put-parameters-mirror";
            var dictionary = new Dictionary<string, string>()
            {
                {"1", "uno"}, {"2", "due"}, {"3", "tre"}
            };
            
            Assert.ThrowsAsync<HttpRequestException>(async () => { await _apiClient.PutCallAPIAsync<SingleResponse<string>>(url, dictionary, UNAUTH_TOKEN); });                        
        }

    }
}
