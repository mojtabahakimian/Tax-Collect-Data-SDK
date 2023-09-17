namespace TaxCollectData.Library.Constants;

internal static class PacketTypeConstants
{
    public const string GET_SERVER_INFORMATION = "server-information";
    public const string INQUIRY_BY_TIME_RANGE = "inquiry";
    public const string INQUIRY_BY_UID = "inquiry-by-uid";
    public const string INQUIRY_BY_REFERENCE_ID = "inquiry-by-reference-id";
    public const string PACKET_TYPE_GET_SERVICE_STUFF_LIST = "GET_SERVICE_STUFF_LIST";
    public const string GET_FISCAL_INFORMATION = "fiscal-information";
    public const string GET_TAXPAYER = "taxpayer";
    public const string INVOICE = "invoice";
    public const string PACKET_TYPE_RECEIVE_INVOICE_CONFIRM = "RECEIVE_INVOICE_CONFIRM";
    public const string NONCE = "nonce";
}