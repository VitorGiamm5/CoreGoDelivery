namespace CoreGoDelivery.Domain.Consts.Regex;

public static class RegexCollectionPatterns
{
    public const string FULL_NAME_PATTERN = @"^[A-Za-z]{2,}\s[A-Za-z]{2,}.*$";
    public const string SPECIAL_CHARACTER_PATTERN = @"[\s\-\.\,]";
    public const string PLATE_FORMAT_OLD = @"^[A-Z]{3}\d{4}$";
    public const string PLATE_FORMAT_NEW = @"^[A-Z]{3}\d{1}[A-Z]{1}\d{2}$";
    public const string GET_EXTENSION_FILE = @"\.(\w+)$";
}
