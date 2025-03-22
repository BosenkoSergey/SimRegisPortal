namespace SimRegisPortal.Persistence.Constants
{
    public class EntityFieldPresets
    {
        public const string DateType = "date";
        public const string DateTimeType = "datetime2";
        public const string GuidType = "uniqueidentifier";

        public const string DefaultGuid = "NEWSEQUENTIALID()";
        public const string DefaultDateTime = "GETUTCDATE()";

        public const int DefaultStringLength = 255;
    }
}
