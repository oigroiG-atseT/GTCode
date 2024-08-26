using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTCode.Services.Loging
{
    public interface ILog
    {

        void Info(Exception exception, params object[] parameters);

        void Info(Exception exception, string parametersInfo, params object[] parameters);

        void Info(string additionalInfo, Exception exception, string parametersInfo, params object[] parameters);


        void Warn(Exception exception, params object[] parameters);

        void Warn(Exception exception, string parametersInfo, params object[] parameters);

        void Warn(string additionalInfo, Exception exception, string parametersInfo, params object[] parameters);


        void Error(Exception exception, params object[] parameters);

        void Error(Exception exception, string parametersInfo, params object[] parameters);

        void Error(string additionalInfo, Exception exception, string parametersInfo, params object[] parameters);


        void Fatal(Exception exception, params object[] parameters);

        void Fatal(Exception exception, string parametersInfo, params object[] parameters);

        void Fatal(string additionalInfo, Exception exception, string parametersInfo, params object[] parameters);

    }
}
