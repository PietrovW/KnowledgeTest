using KnowledgeTest.Models;

namespace KnowledgeTest.Repositorys;

public interface IQuestionRepository
{
    public void Store(Question issue);
    public Question Get(Guid id);
    public IEnumerable<Question> GetAll();
}
