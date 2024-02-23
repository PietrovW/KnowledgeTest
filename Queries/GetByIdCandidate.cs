using Wolverine;

namespace KnowledgeTest.Queries;

public record GetByIdCandidate(Guid Id) : IMessage;