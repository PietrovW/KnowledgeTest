namespace KnowledgeTest.Models;

public record IBaseModel
{
    public Guid Id { get; init; } = Guid.NewGuid();
}
