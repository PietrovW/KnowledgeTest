using KnowledgeTest.Options;
using Microsoft.Extensions.Options;
using Neo4j.Driver;

namespace KnowledgeTest.DAL.DataAccess;

public class Neo4jDataAccess : INeo4jDataAccess
{
    private IAsyncSession _session;
    private ILogger<Neo4jDataAccess> _logger;
    private string _database;
    private readonly IDriver driver;
    private readonly DatabaseNeo4jOptions _option;

    public Neo4jDataAccess(ILogger<Neo4jDataAccess> logger, IOptions<DatabaseNeo4jOptions> databaseNeo4jOptions)
    {
        _option = databaseNeo4jOptions.Value;
        _logger = logger;
        driver = GraphDatabase.Driver(_option.Connection, AuthTokens.Basic(_option.User, _option.Password));
        _database = _option.Database;

        _session = driver.AsyncSession(o => o.WithDatabase(_database));
    }

    public async Task<List<string>> ExecuteReadListAsync(string query, string returnObjectKey, IDictionary<string, object>? parameters = null)
    {
        return await ExecuteReadTransactionAsync<string>(query, returnObjectKey, parameters);
    }

    public async Task<List<Dictionary<string, object>>> ExecuteReadDictionaryAsync(string query, string returnObjectKey, IDictionary<string, object>? parameters = null)
    {
        return await ExecuteReadTransactionAsync<Dictionary<string, object>>(query, returnObjectKey, parameters);
    }

    public async Task<T> ExecuteReadScalarAsync<T>(string query, IDictionary<string, object>? parameters = null)
    {
        try
        {
            parameters = parameters == null ? new Dictionary<string, object>() : parameters;

            var result = await _session.ReadTransactionAsync(async tx =>
            {
                T scalar = default(T);
                var res = await tx.RunAsync(query, parameters);
                scalar = (await res.SingleAsync())[0].As<T>();
                return scalar;
            });

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "There was a problem while executing database query");
            throw;
        }
    }

    /// <summary>
    /// Execute write transaction
    /// </summary>
    public async Task<T> ExecuteWriteTransactionAsync<T>(string query, IDictionary<string, object>? parameters = null)
    {
        try
        {
            parameters = parameters == null ? new Dictionary<string, object>() : parameters;

            var result = await _session.WriteTransactionAsync(async tx =>
            {
                T scalar = default(T);
                var res = await tx.RunAsync(query, parameters);
                scalar = (await res.SingleAsync())[0].As<T>();
                return scalar;
            });

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "There was a problem while executing database query");
            throw;
        }
    }
 
    private async Task<List<T>> ExecuteReadTransactionAsync<T>(string query, string returnObjectKey, IDictionary<string, object>? parameters)
    {
        try
        {
            parameters = parameters == null ? new Dictionary<string, object>() : parameters;

            var result = await _session.ReadTransactionAsync(async tx =>
            {
                var data = new List<T>();
                var res = await tx.RunAsync(query, parameters);
                var records = await res.ToListAsync();
                data = records.Select(x => (T)x.Values[returnObjectKey]).ToList();
                return data;
            });

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "There was a problem while executing database query");
            throw;
        }
    }
 
    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        await _session.CloseAsync();
    }
}
