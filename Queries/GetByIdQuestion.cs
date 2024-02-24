using Wolverine;

namespace KnowledgeTest.Queries;

public record GetByIdQuestion(Guid Id) : IMessage;