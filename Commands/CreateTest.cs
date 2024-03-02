using KnowledgeTest.Models;
using System.ComponentModel.DataAnnotations;
using Wolverine;

namespace KnowledgeTest.Commands;

public record CreateTest(
    [Required] Guid Id,
    [Required] Guid CandidateId,
    [Required] string Name,
    [Required] string Description,
    IList<Question> Questions
    );
