
using OrderManager.Application;

public sealed partial class CreateOrderCommand
{
    private static Results.SuccessResult Success() => new ();
    
    private static Results.FailResult ProviderNotFound(Guid providerId) => new(
        ApplicationErrors.PROVIDER_NOT_FOUND, $"Провайдер с ид '{providerId}' не найден"); 
    
    private static Results.FailResult AlreadyExist(Guid orderId) => new(
        ApplicationErrors.ORDER_ALREADY_EXISTS, $"Заказ с ид '{orderId}' уже существует"); 
    
    private static Results.FailResult AlreadyExist() => new(
        ApplicationErrors.ORDER_ALREADY_EXISTS, $"Такой заказ уже существует"); 
    
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