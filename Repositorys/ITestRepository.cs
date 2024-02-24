using KnowledgeTest.Models;

namespace KnowledgeTest.Repositorys;

public interface ITestRepository
{
    public void Store(Test issue);
    public Test Get(Guid id);
    public IEnumerable<Test> GetAll();
}
