using KnowledgeTest.DAL.DataAccess;
using KnowledgeTest.Models;
using System;

namespace KnowledgeTest.Repositorys;

public class CandidateRepository : ICandidateRepository
{
    private readonly INeo4jDataAccess _neo4jDataAccess;
    private ILogger<CandidateRepository> _logger;
    private readonly Dictionary<Guid, Candidate> _candidates = new();
    public CandidateRepository(INeo4jDataAccess neo4jDataAccess,
        ILogger<CandidateRepository> logger)
    {
        _neo4jDataAccess = neo4jDataAccess;
        _logger = logger;
    }
    public async Task Store(Candidate issue)
    {
        _candidates[issue.Id] = issue;
        var query = @"MERGE (p:Candidate {name: $name}) ON CREATE SET p.email = $email
                            ON MATCH SET p.email = $email ,p.lastName = $lastName, p.id=$id , p.updatedAt = timestamp() RETURN true";
        var parameters = new Dictionary<string, object>
        {
                    { "id", issue.Id.ToString() },
                    { "name", issue.Name },
                    { "email", issue.Email },
                    { "lastName", issue.LastName }
        };
        var result = await _neo4jDataAccess.ExecuteWriteTransactionAsync<bool>(query, parameters);
    }

    public Candidate Get(Guid id)
    {
        if (_candidates.TryGetValue(id, out var candidate))
        {
            return candidate;
        }

        throw new ArgumentOutOfRangeException(nameof(id), "candidates does not exist");
    }

    public IEnumerable<Candidate> GetAll()
    {
        return _candidates.Values;
    }
}
