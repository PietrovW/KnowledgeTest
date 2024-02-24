using KnowledgeTest.Models;
using Wolverine;

namespace KnowledgeTest.Commands;

public record CreateQuestion(
    Guid Id,
    string Contents,
    string Type,
    string DifficultyLevel, 
    string Category, 
    List<Answer> Answers) : ICommand;
