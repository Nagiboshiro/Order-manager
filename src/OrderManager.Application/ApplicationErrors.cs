namespace OrderManager.Application;

public static class ApplicationErrors
{
    public const string PROVIDER_NOT_FOUND = "provider_not_found";
    public const string PROVIDER_ALREADY_EXISTS = "provider_already_exists";
    
    public const string ORDER_NOT_FOUND = "order_not_found";
    public const string ORDER_ALREADY_EXISTS = "order_already_exists";
    
    public const string ORDERITEM_NOT_FOUND = "orderitem_not_found";
    public const string ORDERITEM_ALREADY_EXISTS = "orderitem_already_exists";
    
    public const string ORDER_NUMBER_EQUAL_ORDERITEM_NAME = "order_number_equal_orderitem_name";
        
}