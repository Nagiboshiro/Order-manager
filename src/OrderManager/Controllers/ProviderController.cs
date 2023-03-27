using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManager.Application;
using OrderManager.Application.Commands.Order;
using OrderManager.Application.Commands.Provider;
using OrderManager.Application.Queries;

namespace OrderManager.Controllers;

[ApiController]
[Route("api/providers")]
public sealed class ProviderController : ApiControllerBase
{
    /// <summary>
    /// Create provider
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(201)]
    [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
    [ProducesResponseType(409)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> CreateAsync(
        [FromServices] IMediator mediator,
        [FromBody] CreateProviderModel model,
        CancellationToken cancellationToken = default)
    {
        var commandResult = await mediator.Send(
            new CreateProviderCommand
            {
                ProviderId = model.ProviderId,
                Name = model.Name!,
            }, cancellationToken);
        
        
        return commandResult.Match(
            _ => CreatedAtAction("GetById",
                new { providerId = model.ProviderId },
                new { model.ProviderId }),
            fail => fail.Code switch
            {
                 ApplicationErrors.PROVIDER_NOT_FOUND => Conflict(fail.Code, fail.Message),
                _ => InternalServerError(fail.Message)
            }
        );
        
    }
    
    /// <summary>
    /// Get provider list
    /// </summary>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(GetProvidersListQuery.ProviderView[]), 200)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> GetListAsync(
        [FromServices] IMediator mediator,
        [FromQuery] string? name,
        CancellationToken cancellationToken = default)
    {
        var queryResult = await mediator.Send(
            new GetProvidersListQuery
            {
                Name = name
            }, cancellationToken);

        return queryResult.Match(
            success => Ok(success.Providers),
            fail => fail.Code switch
            {
                _ => InternalServerError(fail.Message)
            }
        );
    }
    
    /// <summary>
    /// Get provider by Id
    /// </summary>
    [HttpGet("{providerId:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> GetByIdAsync(
        [FromServices] IMediator mediator,
        [FromRoute] [Required] Guid providerId,
        CancellationToken cancellationToken = default)
    {
        var queryResult = await mediator.Send(
            new GetProviderByIdQuery
            {
                ProviderId = providerId,
            }, cancellationToken);

        return queryResult.Match(
            success => Ok(success.Provider),
            fail => fail.Code switch
            {
                ApplicationErrors.PROVIDER_NOT_FOUND => NotFound(fail.Code, fail.Message),
                _ => InternalServerError(fail.Message)
            }
        );
    }
    
    /// <summary>
    /// To change the provider data
    /// </summary>
    [HttpPatch("{providerId:guid}/change")]
    [ProducesResponseType(202)]
    [ProducesResponseType(404)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> ChangeAsync(
        [FromServices] IMediator mediator,
        [FromRoute] Guid providerId,
        [FromBody] ChangeProviderModel model,
        CancellationToken cancellationToken = default)
    {
        var queryResult = await mediator.Send(
            new ChangeProviderCommand
            {
                ProviderId = providerId,
                Name = model.Name 
            }, cancellationToken);

        return queryResult.Match(
            _ => NoContent(),
            fail => fail.Code switch
            {
                ApplicationErrors.PROVIDER_NOT_FOUND => NotFound(fail.Code, fail.Message),
                _ => InternalServerError(fail.Message)
            }
        );
    }
    
    /// <summary>
    /// Delete provider
    /// </summary>
    [HttpDelete("{providerId:guid}/delete")]
    [ProducesResponseType(202)]
    [ProducesResponseType(404)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> DeleteAsync(
        [FromServices] IMediator mediator,
        [FromRoute] [Required] Guid providerId,
        CancellationToken cancellationToken = default)
    {
        var queryResult = await mediator.Send(
            new DeleteProviderCommand
            {
                ProviderId = providerId,
            }, cancellationToken);

        return queryResult.Match(
            _ => NoContent(),
            fail => fail.Code switch
            {
                ApplicationErrors.PROVIDER_NOT_FOUND => NotFound(fail.Code, fail.Message),
                _ => InternalServerError(fail.Message)
            }
        );
    }
    
    public sealed class CreateProviderModel
    {
        public Guid ProviderId { get; init; }
        public string? Name { get; init; } = null!;

        public sealed class Validator : AbstractValidator<CreateProviderModel>
        {
            public Validator()
            {
                RuleFor(m => m.Name)
                    .NotEmpty()
                    .MaximumLength(64)
                    .When(d => string.IsNullOrWhiteSpace(d.Name) is false)
                    .WithMessage("Length must be less than 64 or equal to 64");
            }
        }
    }
    
    public sealed class ChangeProviderModel
    {
        public string? Name { get; init; } = null!;
      
        public sealed class Validator : AbstractValidator<ChangeProviderModel>
        {
            public Validator()
            {
                RuleFor(m => m.Name)
                    .NotEmpty()
                    .MaximumLength(64)
                    .When(d => string.IsNullOrWhiteSpace(d.Name) is false)
                    .WithMessage("Length must be less than 64 or equal to 64");
            }
        }
    }
}