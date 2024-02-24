using Wolverine;

namespace KnowledgeTest.Commands;

public record CreateCandidate(
    Guid Id ,
    string Name, 
    string LastName, 
    string Email) :ICommand;