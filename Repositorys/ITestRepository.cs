using KnowledgeTest.Models;

namespace KnowledgeTest.Repositorys;

public interface ITestRepository
{
    public Task Store(Test test);
    public Test Get(Guid id);
    public IEnumerable<Test> GetAll();
}
