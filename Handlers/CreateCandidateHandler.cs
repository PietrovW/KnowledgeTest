using KnowledgeTest.Commands;
using KnowledgeTest.Models;
using KnowledgeTest.Repositorys;

namespace KnowledgeTest.Handlers;

public class CreateCandidateHandler
{
    private readonly ICandidateRepository candidateRepository;

    public CreateCandidateHandler(ICandidateRepository candidateRepository)
    {
        this.candidateRepository = candidateRepository;
    }

    public CandidateCreated Handle(CreateCandidate command)
    {
        this.candidateRepository.Store(new Candidate() {Id=command.Id, Email=command.Email, LastName=command.LastName, Name=command.Name });
        return new CandidateCreated();
    }
}
