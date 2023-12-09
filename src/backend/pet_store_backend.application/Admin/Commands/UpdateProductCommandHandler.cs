using ErrorOr;
using FluentValidation;
using MediatR;
using pet_store_backend.application.Common;
using pet_store_backend.application.Common.Interfaces.Persistence;

namespace pet_store_backend.application.Admin.Commands;

public record UpdateProductCommand(
string ProductName,
    string ProductDetail,
    int ProductQuantity,
    double ProductPrice,
    byte[] ImageData,
    bool Status
) : IRequest<ErrorOr<MessageResult>>;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.ProductName).NotEmpty().MaximumLength(255);
        RuleFor(x => x.ProductDetail).NotEmpty();
        RuleFor(x => x.ProductQuantity).GreaterThan(0);
        RuleFor(x => x.ProductPrice).GreaterThan(0);
        RuleFor(x => x.Status).IsInEnum().WithName("Status").WithMessage("Status must be a boolean value.");
    }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ErrorOr<MessageResult>>
{
    private readonly ICollectionRepository _collectionRepository;
    public UpdateProductCommandHandler(ICollectionRepository collectionRepository)
    {
        _collectionRepository = collectionRepository;
    }
    public async Task<ErrorOr<MessageResult>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        return new MessageResult(request.ProductName);
    }
}