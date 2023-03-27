namespace OrderManager.Client.Extensions;

public static class GeneralTypesExtensions
{
    public static string ToDisplay(this Guid identifier)
    {
        var stringIdentifier = identifier.ToString("N");

        return $"{stringIdentifier[..4]}...{stringIdentifier[^4..]}"; 
    }
}