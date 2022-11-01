namespace GTCode.Services.Api.Response
{
    /// <summary>
    /// Risposta fornita da un server contenente una lista di valori definiti in TType.
    /// </summary>
    /// <typeparam name="TType">Type dei valori contenuti in Data</typeparam>
    public class ListResponse<TType> : GenericResponse
    {

        public List<TType> Data { get; set; } = new List<TType>();

        /// <summary>
        /// Numero totale valori presenti nel server.
        /// </summary>
        public int? TotalCount { get; set; }

    }
}
