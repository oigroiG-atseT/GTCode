namespace GTCode.Utils.Navigation.Pagination
{
    /// <summary>
    /// Metodi per semplificare la paginazione di risultati e la loro navigazione.
    /// </summary>
    public interface IPaginationHandler
    {

        /// <summary>
        /// Numero totale di pagine
        /// </summary>
        int PagineTotali { get; }       

        /// <summary>
        /// Esegue nextPageAction se il numero della pagina corrente è inferiore a quello delle pagine totali.
        /// </summary>
        /// <returns>il numero della nuova pagina corrente</returns>
        int NextPage();

        /// <summary>
        /// Esegue previousPageAction se il numero della pagina corrente è superiore a 1.
        /// </summary>
        /// <returns>il numero della nuova pagina corrente</returns>
        int PreviousPage();

    }
}
