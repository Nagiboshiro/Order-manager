using MediatR;
using OneOf;

namespace OrderManager.Application.Commands.Order.ChangeOrderItemCommand;

public sealed partial class ChangeOrderItemCommand : IRequest<OneOf<
    ChangeOrderItemCommand.Results.SuccessResult, ChangeOrderItemCommand.Results.FailResult>>
{
    public Guid OrderId { get; init; }
    public Guid OrderItemId { get; init; }
    public string? Name { get; init; }
    public decimal? Quantity { get; init; }
    public string? Unit { get; init; }
}