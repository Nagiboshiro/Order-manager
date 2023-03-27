using OrderManager.Integration.Http.Models.OrderItems.Request;
using OrderManager.Integration.Http.Models.OrderItems.Response;
using OrderManager.Integration.Http.Models.Orders;
using OrderManager.Integration.Http.Models.Providers;
using Refit;

namespace OrderManager.Integration.Http;

public interface IOrderManager
{
    /// <summary>
    /// Create provider
    /// </summary>
    /// <returns>
    /// 201 - Created
    /// 400 - Bad Request
    /// 409 - Conflict
    /// </returns>
    [Post("/api/providers")]
    Task<ApiResponse<CreateProviderResponse>> CreateProviderAsync(
        CreateProviderRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get providers list
    /// </summary>
    /// <returns>
    /// 200 - Ok
    /// </returns>
    [Get("/api/providers")]
    Task<ApiResponse<IEnumerable<GetProvidersList>>> GetProvidersAsync(
        GetProvidersListRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get provider by id
    /// </summary>
    /// <returns>
    /// 200 - Ok
    /// </returns>
    [Get("/api/providers/{providerId}")]
    Task<ApiResponse<GetProviderByIdResponse>> GetProviderByIdAsync(
        Guid providerId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// To change the provider data 
    /// </summary>
    /// <returns>
    /// 202 - Ok
    /// </returns>
    [Patch("/api/providers/{providerId}/change")]
    Task<ApiResponse<ChangeProviderResponse>> ChangeProviderAsync(
        Guid providerId,
        ChangeProviderRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete provider
    /// </summary>
    /// <returns>
    /// 202 - Ok
    /// 404 - Not found
    /// </returns>
    [Delete("/api/providers/{providerId}/delete")]
    Task<ApiResponse<DeleteProviderResponse>> DeleteProviderAsync(
        Guid providerId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create order
    /// </summary>
    /// <returns>
    /// 201 - Created
    /// 400 - Bad Request
    /// 409 - Conflict
    /// </returns>
    [Post("/api/orders")]
    Task<ApiResponse<CreateOrderResponse>> CreateOrderAsync(
        CreateOrderRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get orders list
    /// </summary>
    /// <returns>
    /// 200 - Ok
    /// </returns>
    [Get("/api/orders")]
    Task<ApiResponse<IEnumerable<GetOrdersListResponse>>> GetOrdersAsync(
        GetOrdersListRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get order by id
    /// </summary>
    /// <returns>
    /// 200 - Ok
    /// </returns>
    [Get("/api/orders/{orderId}")]
    Task<ApiResponse<GetOrderByIdResponse>> GetOrderByIdAsync(
        Guid orderId,
        GetOrderByIdRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// To change the order data 
    /// </summary>
    /// <returns>
    /// 202 - Ok
    /// </returns>
    [Patch("/api/orders/{orderId}/change")]
    Task<ApiResponse<ChangeOrderResponse>> ChangeOrderAsync(
        Guid orderId,
        ChangeOrderRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete order
    /// </summary>
    /// <returns>
    /// 202 - Ok
    /// 404 - Not found
    /// </returns>
    [Delete("/api/orders/{orderId}/delete")]
    Task<ApiResponse<DeleteOrderResponse>> DeleteOrderAsync(
        Guid orderId,
        CancellationToken cancellationToken = default);
    
    /// 
    /// <summary>
    /// Add orderItem to order
    /// </summary>
    /// <returns>
    /// 201 - Created
    /// 400 - Bad Request
    /// 409 - Conflict
    /// </returns>
    [Post("/api/orders/{orderId}/orderItems")]
    Task<ApiResponse<AddOrderItemToOrderResponse>> AddOrderItemToOrderAsync(
        Guid orderId,
        AddOrderItemToOrderRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// To change the orderItem data 
    /// </summary>
    /// <returns>
    /// 200 - Ok
    /// </returns>
    [Patch("/api/orders/{orderId}/orderItems/{orderItemId}/change")]
    Task<ApiResponse<ChangeOrderItemResponse>> ChangeOrderItemAsync(
        Guid orderId,
        Guid orderItemId,
        ChangeOrderItemRequest request,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Remove order item from order
    /// </summary>
    /// <returns>
    /// 202 - Ok
    /// 404 - Not found
    /// </returns>
    [Delete("/api/orders/{orderId}/orderItems/{orderItemId}/delete")]
    Task<ApiResponse<RemoveOrderItemFromOrderResponse>> RemoveOrderItemFromOrderAsync(
        Guid orderId,
        Guid orderItemId,
        CancellationToken cancellationToken = default);
}