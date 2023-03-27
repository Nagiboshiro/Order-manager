namespace OrderManager.Application.Queries;

public sealed partial class GetProviderByIdQuery
{
    private static Results.SuccessResult Success(ProviderView provider) 
        => new(provider);
    
    private static Results.FailResult NotFound(Guid providerId) => new(
        ApplicationErrors.PROVIDER_NOT_FOUND, $"Провайдер с идентификатором '{providerId}' не найден");

    public static class Results
    {
        public sealed class SuccessResult
        {
            public SuccessResult(ProviderView providers)
            {
                Provider = providers;
            }

            public ProviderView Provider { get; }
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