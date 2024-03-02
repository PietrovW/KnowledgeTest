using FluentValidation;

namespace KnowledgeTest.Queries;

public class GetByIdQuestionValidator : AbstractValidator<GetByIdQuestion>
{
    public GetByIdQuestionValidator()
    {
        RuleFor(x => x.Id).NotNull();
    }
}
