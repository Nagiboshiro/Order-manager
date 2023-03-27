using MediatR;
using OneOf;

public sealed partial class CreateProviderCommand : IRequest<OneOf<
    CreateProviderCommand.Results.SuccessResult, CreateProviderCommand.Results.FailResult>>
{
    public Guid ProviderId { get; init; }
    public string? Name { get; init; }
}