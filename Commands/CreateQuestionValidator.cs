using FluentValidation;

namespace KnowledgeTest.Commands;

public class CreateQuestionValidator : AbstractValidator<CreateQuestion>
{
    public CreateQuestionValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.DifficultyLevel).NotNull();
        RuleFor(x => x.Contents).NotNull();
    }
}
