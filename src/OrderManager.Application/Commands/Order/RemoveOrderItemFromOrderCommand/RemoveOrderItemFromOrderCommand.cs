using MediatR;
using OneOf;

namespace OrderManager.Application.Commands.Order;

public sealed partial class RemoveOrderItemFromOrderCommand : IRequest<OneOf<
    RemoveOrderItemFromOrderCommand.Results.SuccessResult, RemoveOrderItemFromOrderCommand.Results.FailResult>>
{
    public Guid OrderId { get; init; }
    public Guid OrderItemId { get; init; }
}