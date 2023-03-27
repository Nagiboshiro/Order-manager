namespace OrderManager.Domain.Provider;

public sealed class Provider : AggregateRoot
{
    private Provider()
    {
    }

    public Provider(Guid providerId, string name)
        : base(providerId)
    {
        Name = name;
        CreatedOn = UpdatedOn = DateTimeOffset.UtcNow;
    }

    public string Name { get; private set; }
    public DateTimeOffset CreatedOn { get; private set; }
    public DateTimeOffset UpdatedOn { get; private set; }

    public void Change(string? name)
    {
        if (name is null && name == name)
        {
            return;
        }

        Name = name ?? Name;
        UpdatedOn = DateTimeOffset.UtcNow;
    }

}