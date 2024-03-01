using KnowledgeTest.DAL.DataAccess;
using KnowledgeTest.Models;

namespace KnowledgeTest.Repositorys;

public class QuestionRepository : IQuestionRepository
{
    private readonly INeo4jDataAccess _neo4jDataAccess;
    private ILogger<QuestionRepository> _logger;
    public QuestionRepository(INeo4jDataAccess neo4jDataAccess,
        ILogger<QuestionRepository> logger)
    {
        _neo4jDataAccess = neo4jDataAccess;
        _logger = logger;
    }

    public async Task<bool> Insert(Question question, CancellationToken cancellationToken = default(CancellationToken))
    {
        var query = @"MERGE (p:Question {id:$id}) ON CREATE SET p.difficultyLevel = $difficultyLevel, p.contents = $contents,p.answers = $answers
                            ON MATCH SET p.difficultyLevel = $difficultyLevel ,p.contents = $contents,p.answers = $answers, p.id=$id , p.updatedAt = timestamp() RETURN true";

        string output = System.Text.Json.JsonSerializer.Serialize(question.Answers);
        var parameters = new Dictionary<string, object>
        {
                    { "id", question.Id.ToString() },
                    { "difficultyLevel", question.DifficultyLevel },
                    { "contents", question.Contents },
                    { "answers", output }
        };

        return await _neo4jDataAccess.ExecuteWriteTransactionAsync<bool>(query, parameters, cancellationToken: cancellationToken);
    }
    public async Task<Question> GetById(Guid id, CancellationToken cancellationToken = default(CancellationToken))
    {

        var query = @"MATCH (n:Question) WHERE toUpper(n.id) CONTAINS toUpper($id) 
                                RETURN n.id, n.contents, n.difficultyLevel,n.Category, n.answers , n.type ";

        var parameters = new Dictionary<string, object> { { "id", id.ToString() } };

        var persons = await _neo4jDataAccess.ExecuteReadDictionaryAsync(query, parameters, cancellationToken: cancellationToken);
        var result = new List<Question>();

        foreach (var iten in persons)
        {
            result.Add(GetFaktory(iten));
        }
        return result.FirstOrDefault(); //persons;


        // if (_questions.TryGetValue(id, out var question))
        //{
        //  return question;
        // }

      //  throw new ArgumentOutOfRangeException(nameof(id), "questions does not exist");
    }
    Question GetFaktory(IReadOnlyDictionary<string, object> dict)
    {
        return new Question()
        {
            Id = Guid.TryParse(input: dict["n.id"]?.ToString(), out Guid result) ? result : Guid.Empty,
            Type = dict["n.type"]?.ToString(),
            Category = dict["n.Category"]?.ToString(),
            DifficultyLevel = dict["n.difficultyLevel"]?.ToString(),
            // Answers= dict["n.answers"]?.ToString(),
            //{[n.contents, 
        };
    }
    public async Task<IEnumerable<Question>> GetAll(CancellationToken cancellationToken = default(CancellationToken))
    {
        var query = "MATCH (n:Question) RETURN n.id, n.contents , n.type , n.difficultyLevel, n.Category ,n.answers";

        var list = await _neo4jDataAccess.ExecuteReadDictionaryAsync(query: query, cancellationToken: cancellationToken);
        var result = new List<Question>();
        foreach (var iten in list)
        {
            result.Add(GetFaktory(iten));
        }
        return result;
    }

    public async Task<List<Dictionary<string, object>>> SearchPersonsByName(string searchString, CancellationToken cancellationToken = default(CancellationToken))
    {
        var query = @"MATCH (n:Person) WHERE toUpper(n.name) CONTAINS toUpper($searchString) 
                                RETURN p{ name: p.name, born: n.born } ORDER BY p.Name LIMIT 5";

        var parameters = new Dictionary<string, object> { { "searchString", searchString } };

        var persons = await _neo4jDataAccess.ExecuteReadDictionaryAsync(query, parameters, cancellationToken: cancellationToken);

        return persons;
    }
}
