namespace GTCode.Utils.Navigation.Pagination
{
    /// <summary>
    /// Implementazione di base di IPaginationHandler.
    /// </summary>
    public class PaginationHandler : IPaginationHandler
    {

        /// <summary>
        /// Numero totale di pagine
        /// </summary>
        public int PagineTotali { get; private set; }
        
        private int _selectionOffset;        
        private int _paginaCorrente;

        private readonly int _selectionLimit;
        private readonly int _totalCount;
        private readonly Action<int, int> _nextPageAction;
        private readonly Action<int, int> _previousPageAction;

        /// <summary>
        /// Definisce le azioni da intraprendere al momento dello scatto della pagina, più come e quando cambiare pagina.
        /// </summary>
        /// <remarks>
        /// Il primo valore di nextPageAction e previousPageAction è il selectionOffset, mentre il secondo è il selectionLimit.
        /// </remarks>
        /// <param name="nextPageAction">azione da compiere al momento dello scatto ad una pagina successiva</param>
        /// <param name="previousPageAction">azione da compiere al momento dello scatto ad una pagina precedente</param>
        /// <param name="selectionLimit">numero massimo di record per pagina</param>        
        /// <param name="totalCount">numero totale di record che comporranno le pagine</param>
        public PaginationHandler(Action<int, int> nextPageAction, Action<int, int> previousPageAction, int selectionLimit, int totalCount)
        {
            _nextPageAction = nextPageAction;
            _previousPageAction = previousPageAction;
            _selectionLimit = selectionLimit;
            _selectionOffset = 0;
            _totalCount = totalCount;
            _paginaCorrente = 1;
            PagineTotali = this.GetPageCount();
        }

        /// <summary>
        /// Esegue nextPageAction se il numero della pagina corrente è inferiore a quello delle pagine totali.
        /// </summary>
        /// <returns>il numero della nuova pagina corrente</returns>
        public int NextPage()
        {
            var current = _paginaCorrente;
            if (current < PagineTotali)
            {
                _selectionOffset = (_selectionLimit * (current));
                _nextPageAction.Invoke(_selectionOffset, _selectionLimit);
                _paginaCorrente = (++current);
            }
            return _paginaCorrente;
        }

        /// <summary>
        /// Esegue previousPageAction se il numero della pagina corrente è superiore a 1.
        /// </summary>
        /// <returns>il numero della nuova pagina corrente</returns>
        public int PreviousPage()
        {
            var current = _paginaCorrente;
            if (current > 1)
            {
                _selectionOffset = (_selectionLimit * (current - 2));
                _previousPageAction.Invoke(_selectionOffset, _selectionLimit);
                _paginaCorrente = (--current);
            }
            return _paginaCorrente;
        }

        /// <summary>
        /// Calcola il numero di pagine previste in base ai valori di _totalCount e _selectionLimit.
        /// </summary>
        /// <returns>il numero di pagine previste</returns>
        private int GetPageCount()
        {
            double nRecords = (double)_totalCount;
            double number = nRecords / _selectionLimit;
            int nPages = (int)Math.Ceiling(number);
            return (nPages <= 0) ? 1 : nPages;
        }

    }
}
