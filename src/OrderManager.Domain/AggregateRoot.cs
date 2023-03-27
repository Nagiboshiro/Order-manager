namespace OrderManager.Domain;

public abstract class AggregateRoot
{
  protected AggregateRoot()
  {
  }

  protected AggregateRoot(Guid aggregateRootId)
  {
    AggregateRootId = aggregateRootId;
  }

  public Guid AggregateRootId { get; private set; }

}