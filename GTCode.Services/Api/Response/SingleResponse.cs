namespace GTCode.Services.Api.Response
{
    /// <summary>
    /// Risposta fornita da un server contenente un singolo valore definito in TType.
    /// </summary>
    /// <typeparam name="TType">Type del valori contenuto in Data</typeparam>
    public class SingleResponse<TType> : GenericResponse
    {

        public TType? Data { get; set; }

    }
}
