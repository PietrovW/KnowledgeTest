using KnowledgeTest.Models;
using Wolverine;

namespace KnowledgeTest.Commands;

public record CreateTest(
    Guid Id,
    Guid CandidateId,
    string Name,
    string Description,
    IList<Question> Questions
    );
