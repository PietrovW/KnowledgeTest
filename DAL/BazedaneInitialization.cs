using KnowledgeTest.Models;
using Neo4j.Driver;
using System.Text;
namespace KnowledgeTest.DAL;

public class BazedaneInitialization
{
    private readonly IDriver _driver;
    public BazedaneInitialization(IDriver driver)
    {
        _driver = driver;
    }
    public async Task CreateIndices()
    {
        string[] queries = {
                "CREATE INDEX ON :Movie(title)",
                "CREATE INDEX ON :Movie(id)",
                "CREATE INDEX ON :Person(id)",
                "CREATE INDEX ON :Person(name)",
                "CREATE INDEX ON :Genre(name)"
            };

        using var session = _driver.AsyncSession();
        foreach (var query in queries)
        {
            await session.RunAsync(query);
        }
    }

    public async Task CreateCandidate(IList<Candidate> persons)
    {
        string cypher = new StringBuilder()
            .AppendLine("UNWIND {persons} AS person")
            .AppendLine("MERGE (p:Person {name: person.name})")
            .AppendLine("SET p = person")
            .ToString();

        using var session = _driver.AsyncSession();
      
        string output = System.Text.Json.JsonSerializer.Serialize(persons);
        await session.RunAsync(cypher, new Dictionary<string, object>() { { "persons", output } });

    }

    public async Task CreateQuestions(IList<Question> questions)
    {
        string cypher = new StringBuilder()
            .AppendLine("UNWIND {genres} AS genre")
            .AppendLine("MERGE (g:Genre {name: genre.name})")
            .AppendLine("SET g = genre")
            .ToString();

        using var session = _driver.AsyncSession();
        string output = System.Text.Json.JsonSerializer.Serialize(questions);
        await session.RunAsync(cypher, new Dictionary<string, object>() { { "genres", output } });
    }



    //public async Task CreateRelationships(IList<MovieInformation> metadatas)
    //{
    //    string cypher = new StringBuilder()
    //        .AppendLine("UNWIND {metadatas} AS metadata")
    //         // Find the Movie:
    //         .AppendLine("MATCH (m:Movie { title: metadata.movie.title })")
    //         // Create Cast Relationships:
    //         .AppendLine("UNWIND metadata.cast AS actor")
    //         .AppendLine("MATCH (a:Person { name: actor.name })")
    //         .AppendLine("MERGE (a)-[r:ACTED_IN]->(m)")
    //         // Create Director Relationship:
    //         .AppendLine("WITH metadata, m")
    //         .AppendLine("MATCH (d:Person { name: metadata.director.name })")
    //         .AppendLine("MERGE (d)-[r:DIRECTED]->(m)")
    //        // Add Genres:
    //        .AppendLine("WITH metadata, m")
    //        .AppendLine("UNWIND metadata.genres AS genre")
    //        .AppendLine("MATCH (g:Genre { name: genre.name})")
    //        .AppendLine("MERGE (m)-[r:GENRE]->(g)")
    //    .ToString();


    //    using var session = _driver.AsyncSession();
    //    await session.RunAsync(cypher, new Dictionary<string, object>() { { "metadatas", ParameterSerializer.ToDictionary(metadatas) } });
    //}
}
