namespace OrderManager.Application.Commands.Order.ChangeOrderCommand;

public sealed partial class ChangeOrderCommand
{
    private static Results.SuccessResult Success() => new();
    
    private static Results.FailResult NotFound(Guid orderId) => new(
        ApplicationErrors.ORDER_NOT_FOUND, $"Заказ с идентификатором '{orderId}' не найден");


    public static class Results
    {
        public sealed class SuccessResult
        {
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
}