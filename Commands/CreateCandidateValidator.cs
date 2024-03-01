using FluentValidation;

namespace KnowledgeTest.Commands;

public class CreateCandidateValidator : AbstractValidator<CreateCandidate>
{
    public CreateCandidateValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Email).NotNull();
        RuleFor(x => x.LastName).NotNull();
    }
}
