namespace HexaEmployee.Domain.Exceptions
{
    public static class ErrorCode
    {
        public const string InvalidData = "HexaEmployee-1";
        public const string PersistingError = "HexaEmployee-2.";

        public static string Description(string errorCode) => errorCode switch
        {
            InvalidData => "Invalid data.",
            PersistingError => "Error persisting data.",
            _ => "Not specified error.",
        };
    }
}
