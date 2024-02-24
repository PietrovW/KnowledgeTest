using KnowledgeTest.Commands;
using KnowledgeTest.Models;
using KnowledgeTest.Repositorys;

namespace KnowledgeTest.Handlers;

public class CreateTestHandler
{
    private readonly ITestRepository _testRepository;

    public CreateTestHandler(ITestRepository testRepository)
    {
        this._testRepository = testRepository;
    }

    public TestCreated Handle(CreateTest command)
    {
        this._testRepository.Store(new Test() { Id = command.Id, CandidateId=command.CandidateId, Name=command.Name, Questions=command.Questions,  Description=command.Description });
        return new TestCreated();
    }
}
