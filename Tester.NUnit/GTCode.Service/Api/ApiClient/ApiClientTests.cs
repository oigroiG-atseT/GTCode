using GTCode.Services.Api.ApiClient;
using GTCode.Services.Api.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.NUnit.GTCode.Service.Api.ApiClient
{
    [TestFixture]
    public class ApiClientTests
    {

        private readonly IApiClient _apiClient = new ApiClient_HttpClient(new HttpClient());

        private readonly string API_BASE_URL = "http://localhost:3002/gt-code";

        [Category("Api")]
        [Category("GET")]
        [Order(0), Test(Description = "Verifica la corretta esecuzione di GetCallAPIAsync")]
        public void Test000_GetCallAPIAsync()
        {
            string url = $"{API_BASE_URL}/get-people";

            var result = _apiClient.GetCallAPIAsync<ListResponse<string>>(url).Result;

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

            var result = _apiClient.GetCallByteArrayAPIAsync(url).Result;

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

            var result = _apiClient.PostCallAPIAsync<SingleResponse<string>>(url, singleWrapper).Result;

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

            var result = _apiClient.PutCallAPIAsync<SingleResponse<string>>(url, singleWrapper).Result;

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

            var result = _apiClient.PutCallAPIAsync<SingleResponse<string>>(url, dictionary).Result;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Data, Is.EqualTo("uno"));
        }

        [Category("Api")]
        [Category("GET")]
        [Order(5), Test(Description = "Verifica la corretta esecuzione di PutCallAPIAsync(url, Dictionary)")]
        public async Task Test005_DownloadFileAsync()
        {
            string url = $"http://10.10.94.36:8082/texit-report/texit/reportProduttivita/getReportProduttivitaSogei?dataInizio=2023-08-01&dataFine=2023-08-31";


            await _apiClient.DownloadFileAsync(url, "C:\\Users\\giorgio.testa\\Desktop");
            //File.Create("C:\\Users\\giorgio.testa\\Desktop\\AAAAAAAAAAAAAAAAAA.png", result.ReadByte());
            
        }

    }
}
