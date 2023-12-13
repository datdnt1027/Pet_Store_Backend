using pet_store_backend.domain.Entities.PetProducts.PetProductCategory;

namespace pet_store_backend.application.PetProducts.Common;

public record CategoryWithProductCount(
    Category Category,
    int ProductCount
);

public record CategoryResult(
    Guid CategoryId,
    string CategoryName,
    List<ProductBriefResult> Products,
    DateTime CreatedDateTime,
    DateTime UpdatedDateTime
);

public record ProductResult(
    Guid ProductId,
    string ProductName,
    string ProductDetail,
    int ProductQuantity,
    double ProductPrice,
    byte[] ImageData,
    DateTime CreatedDateTime,
    DateTime UpdatedDateTime
);

public record ProductBriefResult(
    Guid ProductId,
    string ProductName,
    double ProductPrice,
    byte[] ImageData
);

public record ProductOrderBriefResult(
    string ProductName,
    string ProductDetail,
    double ProductPrice,
    byte[] ImageData
);
