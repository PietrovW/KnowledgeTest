namespace KnowledgeTest.Options;

public class DatabaseNeo4jOptions
{
    public const string DatabaseNeo4j = nameof(DatabaseNeo4j);
    public Uri Connection { get; set; }

    public string User { get; set; }

    public string Password { get; set; }

    public string Database { get; set; }
}
