using MediatR;
using OneOf;

namespace OrderManager.Application.Commands.Order;

public sealed partial class DeleteOrderCommand : IRequest<OneOf<
    DeleteOrderCommand.Results.SuccessResult, DeleteOrderCommand.Results.FailResult>>
{
    public Guid OrderId { get; init; }
}