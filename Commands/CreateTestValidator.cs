using FluentValidation;

namespace KnowledgeTest.Commands;

public class CreateTestValidator : AbstractValidator<CreateTest>
{
    public CreateTestValidator()
    {
        RuleFor(x => x.Questions).NotNull();
        RuleFor(x => x.CandidateId).NotNull();
    }
}
