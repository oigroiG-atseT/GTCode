using GTCode.Services.Api.Response;
using GTCode.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.NUnit.GTCode.Service.Api.Response
{
    [TestFixture]
    public class ResponseTests
    {

        [Category("Api")]
        [Category("Response")]
        [Order(1), Test(Description = "Verifica la corretta esecuzione di CheckStatus() quando non ci sono errori dal server")]
        public void Test001_CheckStatus_doesNotThrow()
        {
            GenericResponse response = new GenericResponse()
            {
                Success = true, Message = "Success"
            };

            Assert.DoesNotThrow(() => response.CheckStatus());
        }

        [Category("Api")]
        [Category("Response")]
        [Order(2), Test(Description = "Verifica la corretta esecuzione di CheckStatus() quando ci sono errori dal server")]
        public void Test002_CheckStatus_throws()
        {
            GenericResponse response = new GenericResponse()
            {
                Success = false,
                Message = "Errore dal server"
            };

            Assert.Throws<ServerException>(() => response.CheckStatus());
        }

        [Category("Api")]
        [Category("Response")]
        [Order(3), Test(Description = "Verifica la corretta esecuzione di CheckStatus() quando è stato ritornato false come success ma non ci sono errori")]
        public void Test003_CheckStatus_onlyFalse()
        {
            GenericResponse response = new GenericResponse()
            {
                Success = false,
                Message = ""
            };

            Assert.DoesNotThrow(() => response.CheckStatus());
        }

    }
}
