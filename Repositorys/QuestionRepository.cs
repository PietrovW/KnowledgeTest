using KnowledgeTest.Models;

namespace KnowledgeTest.Repositorys;

public class QuestionRepository: IQuestionRepository
{
    private readonly Dictionary<Guid, Question> _questions = new();

    public void Store(Question issue)
    {
        _questions[issue.Id] = issue;
    }

    public Question Get(Guid id)
    {
        if (_questions.TryGetValue(id, out var question))
        {
            return question;
        }

        throw new ArgumentOutOfRangeException(nameof(id), "questions does not exist");
    }

    public IEnumerable<Question> GetAll()
    {
        return _questions.Values;
    }
}
