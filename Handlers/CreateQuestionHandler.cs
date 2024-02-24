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

    public QuestionCreated Handle(CreateQuestion command)
    {
        this._questionRepository.Store(new Question() { Id = command.Id, Category =command.Category, Contents=command.Contents, DifficultyLevel=command.DifficultyLevel,Type=command.Type, Answers=command.Answers });
        return new QuestionCreated();
    }
}
