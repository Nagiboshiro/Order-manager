using MediatR;
using OneOf;

namespace OrderManager.Application.Commands.Provider;

public sealed partial class DeleteProviderCommand : IRequest<OneOf<
    DeleteProviderCommand.Results.SuccessResult, DeleteProviderCommand.Results.FailResult>>
{
    public Guid ProviderId { get; init; }
}