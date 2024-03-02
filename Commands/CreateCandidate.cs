using System.ComponentModel.DataAnnotations;
using Wolverine;

namespace KnowledgeTest.Commands;

public record CreateCandidate(
   [Required] Guid Id ,
   [Required] string Name,
   [Required] string LastName,
   [Required] string Email) :ICommand;