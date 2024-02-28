using KnowledgeTest.DAL.DataAccess;
using KnowledgeTest.Models;
using Neo4j.Driver;
using System;

namespace KnowledgeTest.Repositorys;

public class QuestionRepository: IQuestionRepository
{
    private readonly INeo4jDataAccess _neo4jDataAccess;
    private ILogger<QuestionRepository> _logger;
    public QuestionRepository(INeo4jDataAccess neo4jDataAccess,
        ILogger<QuestionRepository> logger)
    {
        _neo4jDataAccess = neo4jDataAccess;
        _logger = logger;
    }
    
    public async Task Store(Question question)
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

        var result = await _neo4jDataAccess.ExecuteWriteTransactionAsync<bool>(query, parameters);
    }

    public Question Get(Guid id)
    {
       // if (_questions.TryGetValue(id, out var question))
        //{
          //  return question;
       // }

        throw new ArgumentOutOfRangeException(nameof(id), "questions does not exist");
    }

    public async Task<IEnumerable<Question>> GetAll()
    {
        var query = "MATCH (n:Question) RETURN n.contents , n.type , n.difficultyLevel, n.Category ,n.answers";
        
       var test = await _neo4jDataAccess.ExecuteReadListAsync<Question>(query:query,"");
        return test;
    }

    public async Task<List<Dictionary<string, object>>> SearchPersonsByName(string searchString)
    {
        var query = @"MATCH (p:Person) WHERE toUpper(p.name) CONTAINS toUpper($searchString) 
                                RETURN p{ name: p.name, born: p.born } ORDER BY p.Name LIMIT 5";

        IDictionary<string, object> parameters = new Dictionary<string, object> { { "searchString", searchString } };

        var persons = await _neo4jDataAccess.ExecuteReadDictionaryAsync(query, "p", parameters);

        return persons;
    }
}
