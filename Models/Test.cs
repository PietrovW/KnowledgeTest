namespace KnowledgeTest.Models;

public record Test
{
    public Guid Id { get; init; }
    public Guid CandidateId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public IList<Question> Questions { get; init; }
}
