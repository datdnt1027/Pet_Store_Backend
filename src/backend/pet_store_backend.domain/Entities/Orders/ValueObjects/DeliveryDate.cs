using pet_store_backend.domain.Common.Models;

namespace pet_store_backend.domain.Entities.Orders.ValueObjects;

public sealed class DeliveryDate : ValueObject
{
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    private DeliveryDate(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public static DeliveryDate Create(DateTime StartDate, DateTime EndDate)
    {
        return new DeliveryDate(StartDate, EndDate);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return StartDate;
        yield return EndDate;
    }
}
