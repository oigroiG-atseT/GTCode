﻿using GTCode.Services.Api.Response;
using GTCode.Services.Exceptions;
using GTCode.Utils;
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

        [Category("Api")]
        [Category("Response")]
        [Order(4), Test(Description = "Verifica la corretta esecuzione di CheckStatus() nel caso sia stato impostato un ResponseOptions locale")]
        public void Test004_CheckStatus_localOptions()
        {
            var options = new ResponseOptions() { ONLY_THROW_ON_MESSAGE = false };
            GenericResponse response = new GenericResponse()
            {
                Success = false,
                Message = ""
            };
            Assert.That(
                Assert.Throws<ServerException>(() => response.CheckStatus(options)).Message,
                Is.EqualTo("GTC0002: Errore dal server:\n")
            );

            options = new ResponseOptions() { ALWAYS_THROW_AS_SERVER_EXCEPTION = false };
            response = new GenericResponse()
            {
                Success = false,
                Message = "Questa è una notifica, non un eccezione"
            };
            Assert.That(
                Assert.Throws<ServerException>(() => response.CheckStatus(options)).Message, 
                Is.EqualTo("Questa è una notifica, non un eccezione")
            );

            options = new ResponseOptions() { ONLY_THROW_ON_MESSAGE = false, ALWAYS_THROW_AS_SERVER_EXCEPTION = false };
            response = new GenericResponse()
            {
                Success = false,
                Message = ""
            };
            Assert.That(
                Assert.Throws<ServerException>(() => response.CheckStatus(options)).Message,
                Is.EqualTo("GTC0002: Errore dal server")
            );

            options = new ResponseOptions() { ONLY_THROW_ON_MESSAGE = false, ALWAYS_THROW_AS_SERVER_EXCEPTION = false };
            response = new GenericResponse()
            {
                Success = false,
                Message = "Questa è una notifica, non un eccezione"
            };
            Assert.That(
                Assert.Throws<ServerException>(() => response.CheckStatus(options)).Message,
                Is.EqualTo("Questa è una notifica, non un eccezione")
            );
        }

        [Category("Api")]
        [Category("Response")]
        [Order(5), Test(Description = "Verifica la corretta esecuzione di CheckStatus() nel caso sia stato impostato un ResponseOptions globale")]
        public void Test005_CheckStatus_globalOptions()
        {
            ResponseOptions.DEFAULT = new ResponseOptions() { ONLY_THROW_ON_MESSAGE = false, ALWAYS_THROW_AS_SERVER_EXCEPTION = false };
            
            var response = new GenericResponse()
            {
                Success = false,
                Message = ""
            };
            Assert.That(
                Assert.Throws<ServerException>(() => response.CheckStatus()).Message,
                Is.EqualTo("GTC0002: Errore dal server")
            );
            
            response = new GenericResponse()
            {
                Success = false,
                Message = "Questa è una notifica, non un eccezione"
            };
            Assert.That(
                Assert.Throws<ServerException>(() => response.CheckStatus()).Message,
                Is.EqualTo("Questa è una notifica, non un eccezione")
            );
        }

    }
}
