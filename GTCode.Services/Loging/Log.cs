namespace GTCode.Services.Loging
{
    internal class Log
    {

        public string Info { get; set; }
        public string ParametersInfo { get; set; }
        public object[] Parameters { get; set; }
        public string Error { get; set; }
        public string StackTrace { get; set; }

        public Log() { }
        public Log(string info, string parametersInfo, object[] parameters, Exception exception)
        {
            Info = info;
            ParametersInfo = parametersInfo;
            Parameters = parameters;
            Error = exception.Message;
            StackTrace = exception.StackTrace;
        }

    }
}
