using Wolverine;

namespace KnowledgeTest.Commands;

public record CreateCandidate(string Name, string LastName, string Email) :ICommand;