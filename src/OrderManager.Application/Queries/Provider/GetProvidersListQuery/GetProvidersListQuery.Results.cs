namespace OrderManager.Application.Queries;

public sealed partial class GetProvidersListQuery
{
    private static Results.SuccessResult Success(IEnumerable<ProviderView> providers) 
        => new(providers);
    
    public static class Results
    {
        public sealed class SuccessResult
        {
            public SuccessResult(IEnumerable<ProviderView> providers)
            {
                Providers = providers;
            }

            public IEnumerable<ProviderView> Providers { get; }
        }
        
        public sealed class FailResult
        {
            public FailResult(string code, string message)
            {
                Code = code;
                Message = message;
            }

            public string Code { get; }
            public string Message { get; }
        }
    }

    public sealed class ProviderView
    {
        public Guid ProviderId { get; init; }
        public string Name { get; init; } = null!;
        public DateTimeOffset CreatedOn { get; init; }
        public DateTimeOffset UpdatedOn { get; init; }
    }
}