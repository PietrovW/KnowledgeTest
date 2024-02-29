using KnowledgeTest.Commands;
using KnowledgeTest.Models;
using KnowledgeTest.Repositorys;

namespace KnowledgeTest.Handlers;

public class CreateQuestionHandler
{
    private readonly IQuestionRepository _questionRepository;

    public CreateQuestionHandler(IQuestionRepository questionRepository)
    {
        this._questionRepository = questionRepository;
    }

    public async Task<QuestionCreated> Handle(CreateQuestion command, CancellationToken cancellationToken = default(CancellationToken))
    {
        await this._questionRepository.Insert(new Question() { Id = command.Id, Category = command.Category, Contents = command.Contents, DifficultyLevel = command.DifficultyLevel, Type = command.Type, Answers = command.Answers }, cancellationToken: cancellationToken);
        return new QuestionCreated();
    }
}
