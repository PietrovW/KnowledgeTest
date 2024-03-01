using KnowledgeTest.Models;

namespace KnowledgeTest.Repositorys;

public interface ITestRepository
{
    public Task Store(Test test);
    public Test Get(Guid id);
    public Task<IEnumerable<Test>> GetAll(CancellationToken cancellationToken = default(CancellationToken));
}
