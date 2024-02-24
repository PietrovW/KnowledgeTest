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

    public Task<Question> Handle(GetByIdQuestion command)
    {
        return Task.FromResult(_questionRepository.Get(command.Id));
    }
}
