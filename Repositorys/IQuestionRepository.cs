using KnowledgeTest.Models;

namespace KnowledgeTest.Repositorys;

public interface IQuestionRepository
{
    public Task<bool> Insert(Question question, CancellationToken cancellationToken = default(CancellationToken));
    public Task<Question> GetById(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    public Task<IEnumerable<Question>> GetAll(CancellationToken cancellationToken = default(CancellationToken));
}
