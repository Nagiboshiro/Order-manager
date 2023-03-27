using MediatR;
using OneOf;


public sealed partial class CreateOrderCommand : IRequest<OneOf<
    CreateOrderCommand.Results.SuccessResult, CreateOrderCommand.Results.FailResult>>
{
    public Guid ProviderId { get; init; }
    public Guid OrderId { get; init; }
    public string Number { get; init; }
    public DateTimeOffset Date { get; init; }
}