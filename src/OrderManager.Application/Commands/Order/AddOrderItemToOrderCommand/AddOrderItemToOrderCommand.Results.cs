namespace OrderManager.Application.Commands.Order.AddOrderItemToOrderCommand;

public sealed partial class AddOrderItemToOrderCommand
{
    private static Results.SuccessResult Success() => new ();
    
    private static Results.FailResult OrderNotFound(Guid orderId) => new(
        ApplicationErrors.ORDER_NOT_FOUND, $"Заказ с ид '{orderId}' не найден"); 
    
    private static Results.FailResult OrderNumberEqualOrderItemName() => new(
        ApplicationErrors.ORDER_NUMBER_EQUAL_ORDERITEM_NAME, $"Имя элемента закааза не может быть равно номеру заказа"); 
    
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