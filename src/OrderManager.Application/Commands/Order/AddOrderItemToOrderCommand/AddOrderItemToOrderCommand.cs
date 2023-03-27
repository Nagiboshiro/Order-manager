using MediatR;
using OneOf;

namespace OrderManager.Application.Commands.Order.AddOrderItemToOrderCommand;

public sealed partial class AddOrderItemToOrderCommand : IRequest<OneOf<
    AddOrderItemToOrderCommand.Results.SuccessResult, AddOrderItemToOrderCommand.Results.FailResult>>
{
    public Guid OrderId { get; init; }
    public Guid OrderItemId { get; init; }
    public string Name { get; init; }
    public decimal Quantity { get; init; }
    public string Unit { get; init; }
}