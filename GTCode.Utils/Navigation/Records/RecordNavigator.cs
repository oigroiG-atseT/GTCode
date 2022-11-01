namespace GTCode.Utils.Navigation.Records
{
    /// <summary>
    /// Implementazione di base di IRecordNavigator.
    /// </summary>
    /// <typeparam name="T">Type contenuto nella collezione</typeparam>
    public class RecordNavigator<T> : IRecordNavigator<T>
    {

        public int SelectedRecordIndex { get; set; }
        public Func<int, int> LoadRecordAction { get; private set; }

        private int _startIndex;
        private int _itemCount;
        private List<T> _collection;
        
        /// <summary>
        /// Definisce il tipo di collezione e l'azione da intraprendere ad ogni navigazione. 
        /// </summary>
        /// <param name="collection">collezione da navigare</param>
        /// <param name="loadRecordAction">azione da inraprendere ad ogni navigazione</param>
        public RecordNavigator(List<T> collection, Func<int, int> loadRecordAction)
        {
            _collection = collection;
            LoadRecordAction = loadRecordAction;
        }

        public void Start(List<T> collection, int startIndex)
        {
            SelectedRecordIndex = startIndex;
            _startIndex = startIndex;                        
            _collection = collection;
            _itemCount = _collection.Count;
        }

        public void Next()
        {
            if (SelectedRecordIndex < (_itemCount - 1))
            {
                SelectedRecordIndex++;
                LoadRecordAction(SelectedRecordIndex);
            }            
        }

        public void Previous()
        {
            if (SelectedRecordIndex > _startIndex)
            {
                SelectedRecordIndex--;
                LoadRecordAction(SelectedRecordIndex);
            }
        }
    
    }
}
