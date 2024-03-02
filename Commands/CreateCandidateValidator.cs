using FluentValidation;

namespace KnowledgeTest.Commands;

public class GetByIdCandidateValidator : AbstractValidator<CreateCandidate>
{
    public GetByIdCandidateValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Email)
         .NotEmpty()
        .       WithMessage("Email address is required.")
        .EmailAddress()
            .WithMessage("A valid email address is required.");
        RuleFor(x => x.LastName).NotNull();
    }
}
