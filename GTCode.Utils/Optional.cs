namespace GTCode.Utils
{
    /// <summary>
    /// Classe ideata a partire da java.util.Optional già esistente su Java.  
    /// Si tratta di un oggetto container il quale può o non può contenere un valore non-null. 
    /// Se un valore è presente l’oggetto è considerato pieno, altrimenti viene considerato vuoto.
    /// </summary>
    /// <typeparam name="T">Type del valore contenuto</typeparam>
    public sealed class Optional<T> where T : class
    {

        /// <summary>
        /// Se non-null, il valore; se null, indica che nessun valore è presente
        /// </summary>
        private readonly T? _value;

        /// <summary>
        /// Costruisce un instanza vuota.
        /// </summary>
        /// <remarks>
        /// Generalmente solo un instanza vuota, Optional.EMPTY, dovrebbe esistere per VM.
        /// </remarks>
        private Optional() => _value = null;

        /// <summary>
        /// Costruisce un instanza con il valore fornito.
        /// </summary>
        /// <param name="value">il valore not-null che deve essere presente</param>
        /// <exception cref="ArgumentNullException">se il valore è null</exception>
        private Optional(T value)
        {
            if (value is null) throw new ArgumentNullException();
            _value = value;
        }

        /// <summary>
        /// Ritorna un instanza vuota di Optional. Nessun valore è presente per questo Optional.        
        /// </summary>        
        /// <returns>Un Optional vuoto</returns>
        public static Optional<T> Empty() => new Optional<T>();

        /// <summary>
        /// Ritorna un Optional con il valore not-null specificato.
        /// </summary>
        /// <param name="value">il valore che deve essere presente, il quale deve essere not-null</param>
        /// <returns>un Optional con il valore indicato</returns>
        /// <exception cref="ArgumentNullException">se il valore è null</exception>
        public static Optional<T> Of(T value) => new Optional<T>(value);

        /// <summary>
        /// Ritorna un Optional contenente il valore fornito, se not-null, altrimenti ritorna un Optional vuoto.
        /// </summary>
        /// <param name="value">il valore, possibly-null, che deve essere presente</param>
        /// <returns>un Optional con il valore indicato</returns>
        public static Optional<T> OfNullable(T value)
        {
            return (value is null) ? Empty() : Of(value);
        }

        /// <summary>
        /// Se un valore è presente in questo Optional, ritorna il valore, 
        /// altrimenti solleva un InvalidOperationException
        /// </summary>
        /// <returns>il valore not-null contenuto in questo Optional</returns>
        /// <exception cref="InvalidOperationException">se nessun valore è presente</exception>
        /// <see cref="Optional{T}.IsPresent()"/>
        public T Get()
        {
            if (_value is null) throw new InvalidOperationException("Nessun valore presente");
            return _value;
        }

        /// <summary>
        /// Ritorna true se un valore è presente, altrimenti false
        /// </summary>
        /// <returns>true se un valore è presente, altrimenti false</returns>
        public bool IsPresent() => _value is not null;

        /// <summary>
        /// Se un valore è presente, invoca l' Action specificata, altrimenti non fa nulla.
        /// </summary>
        /// <param name="consumer">codice da eseguire se il valore è presente</param>
        /// <exception cref="NullReferenceException">se il valore è presente e <code>consumer</code> è null</exception>
        public void IfPresent(Action<T> consumer)
        {
            if (_value is not null) consumer.Invoke(_value);
        }

        /// <summary>
        /// Se un valore è presente, e il valore soddisfa il predicato fornito,
        /// ritorna un Optional descrivente il valore, altrimenti ritorna un Optional vuoto.
        /// </summary>
        /// <param name="predicate">predicato da applicare al valore, se presente</param>
        /// <returns>un Optional descrivente questo Optional se il valore è presente e soddisfa
        /// il predicato fornito, altrimenti un Optional vuoto</returns>
        /// <exception cref="NullReferenceException">se il predicato è null</exception>
        public Optional<T> Filter(Predicate<T> predicate)
        {
            if (predicate is null) throw new NullReferenceException();
            if (!this.IsPresent()) return this;
            else return predicate.Invoke(_value) ? this : Empty();
        }

        /// <summary>
        /// Ritorna il valore se presente, altrimenti ritorna other.
        /// </summary>
        /// <param name="other">il valore che deve essere restituito se nessun valore è presente;
        /// può essere null</param>
        /// <returns>il valore, se presente, altrimenti other</returns>
        public T OrElse(T other) => (_value is not null) ? _value : other;

        /// <summary>
        /// Ritorna il valore se presente, altrimenti invoca other e ritorna il risultato di tale invocazione.
        /// </summary>
        /// <param name="other">funzione il cui risultato viene ritornato se nessun valore è presente</param>
        /// <exception cref="NullReferenceException">se nessun valore è presente e other è null</exception>
        /// <returns>il valore se presente altrimenti il risultato di  other.Invoke()</returns>
        public T OrElseGet(Func<T> other) => (_value is not null) ? _value : other.Invoke();

        /// <summary>
        /// Ritorna il valore contenuto, se presente, alrimenti solleva un eccezione creata tramite l'exceptionSupplier.
        /// </summary>
        /// <typeparam name="X">Type dell'eccezione da sollevare</typeparam>
        /// <param name="exceptionSupplier">Il supplier che ritornerà l'ecccezione da sollevare</param>
        /// <exception cref="X">se nessun valore è presente</exception>
        /// <exception cref="NullReferenceException">se nessun valore è presente e exceptionSupplier è null</exception>
        /// <returns>il valore di questa instanza</returns>
        public T OrElseThrow<X>(Func<X> exceptionSupplier) where X : Exception
        {
            if (this.IsPresent()) return _value;
            else throw exceptionSupplier.Invoke();
        }

        /// <summary>
        /// Ritorna l'hash code del presente valore, se presente, oppure 0 (zero) se nessun valore è presente.
        /// </summary>
        /// <returns>hash code del presente valore oppure 0 se nessun valore è presente/returns>
        public override int GetHashCode() => _value.GetHashCode();

        /// <summary>
        /// Ritorna una string non-empty rappresentante questo Optional, in una metodologia utile al debugging.
        /// </summary>
        /// <returns>la rappresentazione string di questa instanza</returns>
        public override string ToString() => this.IsPresent() ? String.Format("Optional[%s]", _value) : "Optional.Empty";

    }
}
