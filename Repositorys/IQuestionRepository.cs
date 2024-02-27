using KnowledgeTest.Models;

namespace KnowledgeTest.Repositorys;

public interface IQuestionRepository
{
    public Task Store(Question issue);
    public Question Get(Guid id);
    public Task<IEnumerable<Question>> GetAll();
}
