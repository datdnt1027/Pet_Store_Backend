using pet_store_backend.domain.Common.Models;

namespace pet_store_backend.domain.Entities.PetProducts.ValueObjects;

public sealed class Price : ValueObject
{
    public double Value
    {
        get
        {

            if (Discount != 0)
            {
                return Amount * (1 - Discount);
            }
            return Amount;
        }
    }


    public double Amount { get; private set; }

    public string Currency { get; private set; }

    public float Discount { get; private set; }

    private Price(double amount, string currency, float discount)
    {
        Amount = amount;
        Currency = currency;
        Discount = discount;
    }

    public static Price CreateNew(double amount = 0, float discount = 0, string currency = "VND")
    {
        return new Price(amount, currency, discount);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}