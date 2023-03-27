using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManager.Application;
using OrderManager.Application.Commands.Order;
using OrderManager.Application.Commands.Order.AddOrderItemToOrderCommand;
using OrderManager.Application.Commands.Order.ChangeOrderItemCommand;

namespace OrderManager.Controllers;

[ApiController]
[Route("api/orders/{orderId:guid}/orderItems")]
public sealed class OrderItemController : ApiControllerBase
{
    /// <summary>
    /// Add orderItem to order
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
        [FromRoute] [Required] Guid orderId,
        [FromBody] AddOrderItemToOrderModel model,
        CancellationToken cancellationToken = default)
    {
        var commandResult = await mediator.Send(
            new AddOrderItemToOrderCommand
            {
                OrderId = orderId,
                OrderItemId = model.OrderItemId,
                Name = model.Name,
                Quantity = model.Quantity,
                Unit = model.Unit
            }, cancellationToken);
    
    
        return commandResult.Match(
            _ => Ok(),
            fail => fail.Code switch
            {
                ApplicationErrors.ORDER_NOT_FOUND => Conflict(fail.Code, fail.Message),
                _ => InternalServerError(fail.Message)
            }
        );
    }
    
    /// <summary>
    /// To change the data orderItem
    /// </summary>
    [HttpPatch("{orderItemId:guid}/change")]
    [ProducesResponseType(202)]
    [ProducesResponseType(404)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> ChangeAsync(
        [FromServices] IMediator mediator,
        [FromRoute] [Required] Guid orderId,
        [FromRoute] [Required] Guid orderItemId,
        [FromBody] ChangeOrderItemModel model,
        CancellationToken cancellationToken = default)
    {
        var queryResult = await mediator.Send(
            new ChangeOrderItemCommand
            {
                OrderId = orderId,
                OrderItemId = orderItemId,
                Quantity = model.Quantity,
                Name = model.Name,
                Unit = model.Unit,
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
    
    [HttpDelete("{orderItemId:guid}/delete")]
    [ProducesResponseType(202)]
    [ProducesResponseType(404)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> DeleteAsync(
        [FromServices] IMediator mediator,
        [FromRoute] [Required] Guid orderId,
        [FromRoute] [Required] Guid orderItemId,
        CancellationToken cancellationToken = default)
    {
        var queryResult = await mediator.Send(
            new RemoveOrderItemFromOrderCommand
            {
                OrderId = orderId,
                OrderItemId = orderItemId
            }, cancellationToken);

        return queryResult.Match(
            _ => NoContent(),
            fail => fail.Code switch
            {
                ApplicationErrors.ORDER_NOT_FOUND => NotFound(fail.Code, fail.Message),
                ApplicationErrors.ORDERITEM_NOT_FOUND => NotFound(fail.Code, fail.Message),
                _ => InternalServerError(fail.Message)
            }
        );
    }
    
    public sealed class ChangeOrderItemModel
    {
        public string? Name { get; init; } = null!;
        public decimal? Quantity { get; init; } = null!;
        public string? Unit { get; init; } = null!;

        public sealed class Validator : AbstractValidator<ChangeOrderItemModel>
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
    
    public sealed class AddOrderItemToOrderModel
    {
        public Guid OrderItemId { get; init; }
        public decimal Quantity { get; init; }
        public string Unit { get; init; }
        public string Name { get; init; }

        public sealed class Validator : AbstractValidator<AddOrderItemToOrderModel>
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