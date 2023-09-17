namespace TaxCollectData.Library.Exceptions
{
    internal class TaxApiException : Exception
    {
        public TaxApiException()
        {
        }

        public TaxApiException(Exception cause) : base(cause.Message, cause)
        {
        }

        public TaxApiException(string message) : base(message)
        {
        }

        public TaxApiException(string message, Exception cause) : base(message, cause)
        {
        }
    }
}