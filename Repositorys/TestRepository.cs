using KnowledgeTest.DAL.DataAccess;
using KnowledgeTest.Models;
using System.Threading;

namespace KnowledgeTest.Repositorys;

public class TestRepository: ITestRepository
{
    private readonly Dictionary<Guid, Test> _tests = new();

    private readonly INeo4jDataAccess _neo4jDataAccess;
    private ILogger<CandidateRepository> _logger;

    public TestRepository(INeo4jDataAccess neo4jDataAccess,
        ILogger<CandidateRepository> logger)
    {
        _neo4jDataAccess = neo4jDataAccess;
        _logger = logger;
    }

    public async Task Store(Test test)
    {
        _tests[test.Id] = test;

        var query = @"MERGE (p:Test {name: $name})  ON CREATE SET p.id = $id, p.candidateId = $candidateId
                            ON MATCH SET p.candidateId = $candidateId ,p.description = $description,p.questions = $questions, p.id=$id , p.updatedAt = timestamp() RETURN true";

        string output = System.Text.Json.JsonSerializer.Serialize(test.Questions);
        var parameters = new Dictionary<string, object>
        {
                    { "id", test.Id.ToString() },
                    { "candidateId", test.CandidateId.ToString() },
                    { "name", test.Name },
                    { "description", test.Description },
                    { "questions", output }
        };

        var result = await _neo4jDataAccess.ExecuteWriteTransactionAsync<bool>(query, parameters);

    }

    public Test Get(Guid id)
    {
        if (_tests.TryGetValue(id, out var test))
        {
            return test;
        }

        throw new ArgumentOutOfRangeException(nameof(id), "questions does not exist");
    }
    Test GetFaktory(IReadOnlyDictionary<string, object> dict)
    {
        return new Test()
        {
            Id = Guid.TryParse(input: dict["n.id"]?.ToString(), out Guid result) ? result : Guid.Empty,
           CandidateId = Guid.Parse(dict["n.CandidateId"]?.ToString()),
            Description = dict["n.Description"]?.ToString(),
           Name  = dict["n.Name"]?.ToString(),
         //  Questions = dict["n.Questions"]?.ToString(),
            // Answers= dict["n.answers"]?.ToString(),
            //{[n.contents, 
        };
    }
    public  async Task<IEnumerable<Test>> GetAll(CancellationToken cancellationToken = default(CancellationToken))
    {
        var query = "MATCH (n:Question) RETURN n.id, n.contents , n.type , n.difficultyLevel, n.Category ,n.answers";

        var list = await _neo4jDataAccess.ExecuteReadDictionaryAsync(query: query, cancellationToken: cancellationToken);
        var result = new List<Test>();
        foreach (var iten in list)
        {
            result.Add(GetFaktory(iten));
        }
        return result;
    }
}
