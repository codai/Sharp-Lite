namespace MyStore.Domain
{
    /// <summary>
    /// Provides a generic typed mechanism for returning success/failure feedback along with a value
    /// </summary>
    public class ActionConfirmation<T>
    {
        private ActionConfirmation(string message) {
            Message = message;
        }

        public static ActionConfirmation<T> CreateSuccessConfirmation(string message, T value) {
            return new ActionConfirmation<T>(message) {
                WasSuccessful = true,
                Value = value
            };
        }

        public static ActionConfirmation<T> CreateFailureConfirmation(string message, T value) {
            return new ActionConfirmation<T>(message) {
                WasSuccessful = false,
                Value = value
            };
        }

        public bool WasSuccessful { get; private set; }
        public string Message { get; set; }
        public T Value { get; set; }
    }
}
