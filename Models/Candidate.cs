namespace KnowledgeTest.Models;

public record Candidate
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    //public IList<Test> Tests { get; init; }
}
