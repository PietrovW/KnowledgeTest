using KnowledgeTest.Options;
using Microsoft.Extensions.Options;
using Neo4j.Driver;
using System.Text.Json;

namespace KnowledgeTest.DAL.DataAccess;

public class Neo4jDataAccess : INeo4jDataAccess
{
    private IAsyncSession _session;
    private ILogger<Neo4jDataAccess> _logger;
    private string _database;
    private readonly IDriver driver;
    private readonly DatabaseNeo4jOptions _option;
    private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        
    };

    public Neo4jDataAccess(ILogger<Neo4jDataAccess> logger, IOptions<DatabaseNeo4jOptions> databaseNeo4jOptions)
    {
        _option = databaseNeo4jOptions.Value;
        _logger = logger;
        driver = GraphDatabase.Driver(_option.Connection, AuthTokens.Basic(_option.User, _option.Password));
        _database = _option.Database;

        _session = driver.AsyncSession(o => o.WithDatabase(_database));
    }

    public async Task<List<string>> ExecuteReadListAsync(string query, string returnObjectKey, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await ExecuteReadTransactionAsync<string>(query: query, parameters: parameters, cancellationToken: cancellationToken);
    }

    public async Task<List<T>> ExecuteReadListAsync<T>(string query, string returnObjectKey, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await ExecuteReadTransactionAsync<T>(query: query, parameters:parameters, cancellationToken: cancellationToken);
    }
    public async Task<List<Dictionary<string, object>>> ExecuteReadDictionaryAsync(string query, string returnObjectKey, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await ExecuteReadTransactionAsync<Dictionary<string, object>>(query,  parameters: parameters, cancellationToken: cancellationToken);
    }

    public async Task<T> ExecuteReadScalarAsync<T>(string query, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            parameters = parameters == null ? new Dictionary<string, object>() : parameters;

            var result = await _session.ExecuteReadAsync(async tx =>
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


    public async Task<T> ExecuteWriteTransactionAsync<T>(string query, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            parameters = parameters == null ? new Dictionary<string, object>() : parameters;

            var result = await _session.ExecuteWriteAsync(async tx =>
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
    string MyDictionaryToJson(IReadOnlyDictionary<string,object> dict)
    {
        var entries = dict.Select(d =>
            string.Format("\"{0}\": \"{1}\"", d.Key.Replace("n.",""), string.Join(",", d.Value)));
        return "{" + string.Join(",", entries).Replace("\"[","[").Replace("]\"", "]") + "}";
    }
    private async Task<List<T>> ExecuteReadTransactionAsync<T>(string query, IDictionary<string, object>? parameters, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            parameters = parameters == null ? new Dictionary<string, object>() : parameters;

            var result = await _session.ExecuteReadAsync(async tx =>
            {
                  var data = new List<T>();
                    var res = await tx.RunAsync(query, parameters);
                    var records = await res.ToListAsync();
                    var teste = records.Select(x => x.Values).ToList();
                    foreach (var iten in teste)
                    {
                    try
                    {

                        data.Add(JsonSerializer.Deserialize<T>(MyDictionaryToJson(iten), options: jsonSerializerOptions));
                        // data.Add(JsonSerializer.Deserialize<T>(iten.Values));
                    }
                    catch (Exception ex)
                    {

                    }
                }
              
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
