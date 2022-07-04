using FluentValidation;
using MediatR;

namespace Order.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommand : IRequest<Unit>
{
    public int Id { get; init; }
}

public sealed class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty()
            .NotNull().WithMessage("Id is required");
    }
}