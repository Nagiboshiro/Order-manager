using MediatR;
using OneOf;

namespace OrderManager.Application.Commands.Provider;

public sealed partial class ChangeProviderCommand 
{
    private static Results.SuccessResult Success() => new();

    private static Results.FailResult NotFound(Guid providerId) => new(
        ApplicationErrors.PROVIDER_NOT_FOUND, $"Провайдер с идентификатором '{providerId}' не найден");
    public static class Results
    {
        public sealed class SuccessResult
        {
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