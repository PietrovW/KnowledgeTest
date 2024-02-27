using KnowledgeTest.Models;

namespace KnowledgeTest.Repositorys;

public interface ICandidateRepository
{
    public Task Store(Candidate issue);
    public Candidate Get(Guid id);
    public IEnumerable<Candidate> GetAll();
}
