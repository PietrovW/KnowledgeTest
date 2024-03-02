using FluentValidation;

namespace KnowledgeTest.Commands;

public class CreateTestValidator : AbstractValidator<CreateTest>
{
    public CreateTestValidator()
    {
        RuleFor(x => x.Description).NotEmpty().NotNull();
        RuleFor(x => x.Name).NotEmpty().NotNull();
        RuleFor(x => x.Questions).NotNull();
        RuleFor(x => x.CandidateId).NotNull();
    }
}
