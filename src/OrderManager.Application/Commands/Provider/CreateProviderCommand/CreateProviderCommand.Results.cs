
using OrderManager.Application;

public sealed partial class CreateProviderCommand
{
    private static Results.SuccessResult Success() => new ();

    private static Results.FailResult AlreadyExist(Guid providerId) => new(
        ApplicationErrors.PROVIDER_ALREADY_EXISTS, $"Провайдер с ид '{providerId}' уже существует"); 
    
    public static class Results
    {
        public sealed class SuccessResult {}

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
}