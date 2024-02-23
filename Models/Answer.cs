namespace KnowledgeTest.Models;

public record Answer
{
    public string Contents { get; init; }
    public bool IsCorrect { get; init; }
}
