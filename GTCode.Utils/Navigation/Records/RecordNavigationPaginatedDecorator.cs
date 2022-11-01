namespace GTCode.Utils.Navigation.Records
{
    /// <summary>
    /// Decorator volto alla semplificazione della navigazione paginata utilizzando IRecordNavigator.
    /// </summary>
    /// <typeparam name="T">Type degli elementi contenuti nella collezione di IRecordNavigator</typeparam>
    public class RecordNavigationPaginatedDecorator<T>
    {

        private readonly int _pagineTotali;        
        private readonly int _selectionLimit;
        private readonly Action _nextPageAction;
        private readonly Action _previousPageAction;
        private readonly IRecordNavigator<T> _recordNavigator;

        /// <summary>
        /// Definisce le azioni da intraprendere al momento dello scatto della pagina, più quando cambiare pagina.
        /// </summary>
        /// <param name="recordNavigator">il record navigator da wrappare</param>
        /// <param name="nextPageAction">azione da intraprendere per caricare la pagina successiva</param>
        /// <param name="previousPageAction">azione da intraprendere per caricare la pagina precedente</param>
        /// <param name="selectionLimit">tetto massimo di elementi da caicare nelle pagine</param>
        /// <param name="totalPages">pagine totali da aspettarsi</param>
        public RecordNavigationPaginatedDecorator(IRecordNavigator<T> recordNavigator, Action nextPageAction, Action previousPageAction, int selectionLimit, int totalPages)
        {
            _recordNavigator = recordNavigator;
            _nextPageAction = nextPageAction;
            _previousPageAction = previousPageAction;
            _selectionLimit = selectionLimit;
            _pagineTotali = totalPages;
        }

        /// <summary>
        /// Avvia la navigazione della collezione fornita partendo dalla posizione indicata con startIndex.
        /// Non sarà possibile navigare sotto tale posizione.
        /// </summary>
        /// <remarks>
        /// Al modificarsi della collezione, principalmente nel caso il numero di elementi cambi, è necessario eseguire nuovamente lo Start().
        /// </remarks>
        /// <param name="collection">collezione da navigare</param>
        /// <param name="startIndex">posizione di partenza ed estremo inferiore della navigazione</param> 
        public void Start(List<T> collection, int startIndex)
        {
            _recordNavigator.Start(collection, startIndex);
        }

        /// <summary>
        /// Carica il record successivo se possibile, altrimenti (se richiesto) carica il primo record della pagina successiva.
        /// </summary>
        /// <param name="paginaCorrente">pagina corrente della paginazione</param>
        public void Next(int paginaCorrente)
        {
            _recordNavigator.Next();
            if (_recordNavigator.SelectedRecordIndex == _selectionLimit && paginaCorrente < _pagineTotali)
            {
                _nextPageAction.Invoke();
                _recordNavigator.SelectedRecordIndex = 0;
                _recordNavigator.LoadRecordAction(_recordNavigator.SelectedRecordIndex);
            }
        }

        /// <summary>
        /// Carica il record precedente se possibile, altrimenti (se richiesto) carica l'ultimo record della pagina precedente.
        /// </summary>
        /// <param name="paginaCorrente">pagina corrente della paginazione</param>
        public void Previous(int paginaCorrente)
        {
            _recordNavigator.Previous();
            if (_recordNavigator.SelectedRecordIndex == 0 && paginaCorrente > 1)
            {
                _previousPageAction.Invoke();
                _recordNavigator.SelectedRecordIndex = _selectionLimit - 1;
                _recordNavigator.LoadRecordAction(_recordNavigator.SelectedRecordIndex);
            }
        }

    }
}
