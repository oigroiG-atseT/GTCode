namespace GTCode.Utils.Navigation.Records
{
    /// <summary>
    /// Metodi per semplificare la navigazione di una collezione.
    /// </summary>
    /// <typeparam name="T">Type degli item della collezione</typeparam>
    public interface IRecordNavigator<T>
    {

        /// <summary>
        /// L'indice del record attualemente selezionato.
        /// </summary>
        int SelectedRecordIndex { get; set; }

        /// <summary>
        /// La funzione da eseguire alla navigazione di ogni record.
        /// </summary>
        Func<int, int> LoadRecordAction { get; }

        /// <summary>
        /// Avvia la navigazione della collezione fornita partendo dalla posizione indicata con startIndex.
        /// Non sarà possibile navigare sotto tale posizione.
        /// </summary>
        /// <remarks>
        /// Al modificarsi della collezione, principalmente nel caso il numero di elementi cambi, è necessario eseguire nuovamente lo Start().
        /// </remarks>
        /// <param name="collection">collezione da navigare</param>
        /// <param name="startIndex">posizione di partenza ed estremo inferiore della navigazione</param>                
        void Start(List<T> collection, int startIndex);

        /// <summary>
        /// Carica il record successivo se possibile.
        /// </summary>
        void Next();

        /// <summary>
        /// Carica il record precedente se possibile.
        /// </summary>
        void Previous();

    }
}
