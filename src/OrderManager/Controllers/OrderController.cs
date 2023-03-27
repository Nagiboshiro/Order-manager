using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManager.Application;
using OrderManager.Application.Commands.Order;
using OrderManager.Application.Commands.Order.ChangeOrderCommand;
using OrderManager.Application.Queries;
using OrderManager.Application.Queries.Order.GetOrderByIdQuery;

namespace OrderManager.Controllers;

[ApiController]
[Route("api/orders")]
public sealed class OrderController : ApiControllerBase
{
    /// <summary>
    /// Create order
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
        [FromBody] CreateOrderModel model,
        CancellationToken cancellationToken = default)
    {
        var commandResult = await mediator.Send(
            new CreateOrderCommand
            {
                ProviderId = model.ProviderId,
                OrderId = model.OrderId,
                Number = model.Number,
                Date = model.Date
            }, cancellationToken);
    
    
        return commandResult.Match(
            _ => CreatedAtAction("GetById",
                new { orderId = model.OrderId },
                new { model.OrderId }),
            fail => fail.Code switch
            {
                ApplicationErrors.ORDER_NOT_FOUND => Conflict(fail.Code, fail.Message),
                _ => InternalServerError(fail.Message)
            }
        );
    }
    
    /// <summary>
    /// Get order list
    /// </summary>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(GetOrdersListQuery.OrderView[]), 200)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> GetListAsync(
        [FromServices] IMediator mediator,
        [FromQuery] string? number,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate,
    CancellationToken cancellationToken = default)
    {
        var queryResult = await mediator.Send(
            new GetOrdersListQuery
            {
                Number = number,
                StartDate = startDate,
                EndDate = endDate
            }, cancellationToken);

        return queryResult.Match(
            success => Ok(success.Orders),
            fail => fail.Code switch
            {
                _ => InternalServerError(fail.Message)
            }
        );
    }
    
    /// <summary>
    /// Get order by Id
    /// </summary>
    [HttpGet("{orderId:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> GetByIdAsync(
        [FromServices] IMediator mediator,
        [FromRoute] [Required] Guid orderId,
        [FromQuery] string? name,
        [FromQuery] string? unit,
        CancellationToken cancellationToken = default)
    {
        var queryResult = await mediator.Send(
            new GetOrderByIdQuery
            {
                OrderId = orderId,
                Name = name,
                Unit = unit
            }, cancellationToken);

        return queryResult.Match(
            success => Ok(success.Order),
            fail => fail.Code switch
            {
                ApplicationErrors.PROVIDER_NOT_FOUND => NotFound(fail.Code, fail.Message),
                _ => InternalServerError(fail.Message)
            }
        );
    }
    
    /// <summary>
    /// To change the order data 
    /// </summary>
    [HttpPatch("{orderId:guid}/change")]
    [ProducesResponseType(202)]
    [ProducesResponseType(404)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> ChangeAsync(
        [FromServices] IMediator mediator,
        [FromRoute] Guid orderId,
        [FromBody] ChangeOrderModel model,
        CancellationToken cancellationToken = default)
    {
        var queryResult = await mediator.Send(
            new ChangeOrderCommand
            {
                OrderId = orderId,
                ProviderId = model.ProviderId,
                Date = model.Date,
                Number = model.Number,
            }, cancellationToken);

        return queryResult.Match(
            _ => NoContent(),
            fail => fail.Code switch
            {
                ApplicationErrors.ORDER_NOT_FOUND => NotFound(fail.Code, fail.Message),
                _ => InternalServerError(fail.Message)
            }
        );
    }
    
    /// <summary>
    /// Delete order
    /// </summary>
    [HttpDelete("{orderId:guid}/delete")]
    [ProducesResponseType(202)]
    [ProducesResponseType(404)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> DeleteAsync(
        [FromServices] IMediator mediator,
        [FromRoute] [Required] Guid orderId,
        CancellationToken cancellationToken = default)
    {
        var queryResult = await mediator.Send(
            new DeleteOrderCommand
            {
                OrderId = orderId,
            }, cancellationToken);

        return queryResult.Match(
            _ => NoContent(),
            fail => fail.Code switch
            {
                ApplicationErrors.ORDER_NOT_FOUND => NotFound(fail.Code, fail.Message),
                _ => InternalServerError(fail.Message)
            }
        );
    }
    
    public sealed class CreateOrderModel
    {
        public Guid OrderId { get; init; }
        public Guid ProviderId { get; init; }
        public string Number { get; init; } = null!;
        public DateTimeOffset Date { get; init; }

        public sealed class Validator : AbstractValidator<CreateOrderModel>
        {
            public Validator()
            {
                RuleFor(m => m.Number)
                    .NotEmpty()
                    .MaximumLength(64)
                    .When(d => string.IsNullOrWhiteSpace(d.Number) is false)
                    .WithMessage("Length must be less than 64 or equal to 64");
            }
        }
    }
    
    public sealed class ChangeOrderModel
    {
        public Guid? ProviderId { get; init; }
        public string? Number { get; init; } = null!;
        public DateTimeOffset? Date { get; init; }

        public sealed class Validator : AbstractValidator<ChangeOrderModel>
        {
            public Validator()
            {
                RuleFor(m => m.Number)
                    .NotEmpty()
                    .MaximumLength(64)
                    .When(d => string.IsNullOrWhiteSpace(d.Number) is false)
                    .WithMessage("Length must be less than 64 or equal to 64");
            }
        }
    }
}