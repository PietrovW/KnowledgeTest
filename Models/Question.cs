namespace KnowledgeTest.Models;

public record Question: IBaseModel
{
    public string Contents { get; init; }
    public string Type { get; init; }
    public string DifficultyLevel { get; init; }
    public string Category { get; init; }
    public List<Answer> Answers{ get; init; }
}
