using MediatR;
using OneOf;

namespace OrderManager.Application.Commands.Provider;

public sealed partial class ChangeProviderCommand : IRequest<OneOf<
    ChangeProviderCommand.Results.SuccessResult, ChangeProviderCommand.Results.FailResult>>
{
    public Guid ProviderId { get; init; }
    public string Name { get; init; }
}