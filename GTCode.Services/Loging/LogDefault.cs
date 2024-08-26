using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTCode.Services.Loging
{
    internal class LogDefault : ILog
    {

        public LogDefault(string filePath) {

        }

        public void Info(Exception exception, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void Info(Exception exception, string parametersInfo, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void Info(string additionalInfo, Exception exception, string parametersInfo, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void Warn(Exception exception, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void Warn(Exception exception, string parametersInfo, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void Warn(string additionalInfo, Exception exception, string parametersInfo, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception exception, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception exception, string parametersInfo, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void Error(string additionalInfo, Exception exception, string parametersInfo, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void Fatal(Exception exception, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void Fatal(Exception exception, string parametersInfo, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string additionalInfo, Exception exception, string parametersInfo, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        // file, file.old; quando file raggiunge la soglia scambia i due file e riparte da 0

        private void write()
        {

        }

    }
}
