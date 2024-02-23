using KnowledgeTest.Models;
using KnowledgeTest.Repositorys;
using Wolverine;

namespace KnowledgeTest.Queries;

public class GetAllCandidateHandler
{
    private readonly ICandidateRepository _candidateRepository;

    public GetAllCandidateHandler(ICandidateRepository candidateRepository)
    {
        this._candidateRepository = candidateRepository;
    }

    public Task<IEnumerable<Candidate>> Handle(GetAllCandidate command)
    {
        return Task.FromResult(_candidateRepository.GetAllCandidates());
    }
}
