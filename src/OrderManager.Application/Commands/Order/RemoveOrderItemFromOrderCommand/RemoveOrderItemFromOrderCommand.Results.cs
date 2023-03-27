namespace OrderManager.Application.Commands.Order;

public sealed partial class RemoveOrderItemFromOrderCommand
{
    private static Results.SuccessResult Success() => new();

    private static Results.FailResult OrderNotFound(Guid orderId) => new(
        ApplicationErrors.ORDER_NOT_FOUND, $"Заказ с идентификатором '{orderId}' не найден");

    private static Results.FailResult OrderItemNotFound(Guid orderItemId) => new(
        ApplicationErrors.ORDERITEM_NOT_FOUND, $"Элемент заказа с идентификатором '{orderItemId}' не найден");

    public static class Results
    {
        public sealed class SuccessResult
        {
            public SuccessResult()
            {
            }

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