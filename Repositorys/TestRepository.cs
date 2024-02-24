using KnowledgeTest.Models;

namespace KnowledgeTest.Repositorys;

public class TestRepository: ITestRepository
{
    private readonly Dictionary<Guid, Test> _tests = new();

    public void Store(Test issue)
    {
        _tests[issue.Id] = issue;
    }

    public Test Get(Guid id)
    {
        if (_tests.TryGetValue(id, out var test))
        {
            return test;
        }

        throw new ArgumentOutOfRangeException(nameof(id), "questions does not exist");
    }

    public IEnumerable<Test> GetAll()
    {
        return _tests.Values;
    }
}
