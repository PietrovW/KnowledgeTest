using KnowledgeTest.Models;

namespace KnowledgeTest.Repositorys;

public class CandidateRepository: ICandidateRepository
{
    private readonly Dictionary<Guid, Candidate> _candidates = new();

    public void Store(Candidate issue)
    {
        _candidates[issue.Id] = issue;
    }

    public Candidate Get(Guid id)
    {
        if (_candidates.TryGetValue(id, out var candidate))
        {
            return candidate;
        }

        throw new ArgumentOutOfRangeException(nameof(id), "candidates does not exist");
    }

    public IEnumerable<Candidate> GetAll() {
    
        return _candidates.Values;
    }
}
