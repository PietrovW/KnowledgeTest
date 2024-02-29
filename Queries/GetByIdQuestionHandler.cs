using KnowledgeTest.Models;
using KnowledgeTest.Repositorys;

namespace KnowledgeTest.Queries;

public class GetByIdQuestioneHandler
{
    private readonly IQuestionRepository _questionRepository;

    public GetByIdQuestioneHandler(IQuestionRepository questionRepository)
    {
        this._questionRepository = questionRepository;
    }

    public async Task<Question> Handle(GetByIdQuestion command, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _questionRepository.GetById(command.Id, cancellationToken: cancellationToken);
    }
}
