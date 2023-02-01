namespace Common.Converters;

public static class StringConvert
{
    public static Guid ToGuid(string value)
    {
        return Guid.Parse(value);
    }
}