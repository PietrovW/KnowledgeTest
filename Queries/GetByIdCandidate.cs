using System.ComponentModel.DataAnnotations;
using Wolverine;

namespace KnowledgeTest.Queries;

public record GetByIdCandidate([Required] Guid Id) : IMessage;