using KnowledgeTest.Models;
using KnowledgeTest.Repositorys;

namespace KnowledgeTest.Queries;

public class GetAllQuestionHandler
{
    private readonly IQuestionRepository _questionRepository;

    public GetAllQuestionHandler(IQuestionRepository questionRepository)
    {
        this._questionRepository = questionRepository;
    }

    public Task<IEnumerable<Question>> Handle(GetAllQuestion command)
    {
        return Task.FromResult(_questionRepository.GetAll());
    }
}
