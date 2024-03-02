using FluentValidation;

namespace KnowledgeTest.Queries;

public class GetByIdCandidateValidator : AbstractValidator<GetByIdCandidate>
{
    public GetByIdCandidateValidator()
    {
        RuleFor(x => x.Id).NotNull();
    }
}
