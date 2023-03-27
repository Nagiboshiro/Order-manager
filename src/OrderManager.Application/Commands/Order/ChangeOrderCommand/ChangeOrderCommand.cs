using MediatR;
using OneOf;

namespace OrderManager.Application.Commands.Order.ChangeOrderCommand;

public sealed partial class ChangeOrderCommand : IRequest<OneOf<
    ChangeOrderCommand.Results.SuccessResult, ChangeOrderCommand.Results.FailResult>>
{
    public Guid OrderId { get; init; }
    public Guid? ProviderId { get; init; }
    public string? Number { get; init; }
    public DateTimeOffset? Date { get; init; }
}