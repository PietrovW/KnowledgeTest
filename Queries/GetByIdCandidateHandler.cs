using KnowledgeTest.Models;
using KnowledgeTest.Repositorys;

namespace KnowledgeTest.Queries;

public class GetByIdCandidateHandler
{
    private readonly ICandidateRepository _candidateRepository;

    public GetByIdCandidateHandler(ICandidateRepository candidateRepository)
    {
        this._candidateRepository = candidateRepository;
    }

    public Task<Candidate> Handle(GetByIdCandidate command)
    {
        return Task.FromResult(_candidateRepository.Get(command.Id));
    }
}
