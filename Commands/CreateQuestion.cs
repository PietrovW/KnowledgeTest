using KnowledgeTest.Models;
using System.ComponentModel.DataAnnotations;
using Wolverine;

namespace KnowledgeTest.Commands;

public record CreateQuestion(
    [Required] Guid Id,
    [Required] string Contents,
    [Required] string Type,
    [Required] string DifficultyLevel,
    [Required] string Category, 
    List<Answer> Answers) : ICommand;
