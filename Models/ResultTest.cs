namespace KnowledgeTest.Models;

public record ResultTest
{
    public Candidate Candidate { get; init; }
    public Test Test { get; init; }
    public double Result { get; init; }
}
