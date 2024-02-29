namespace KnowledgeTest.DAL.DataAccess;

public interface INeo4jDataAccess : IAsyncDisposable
{
    Task<List<string>> ExecuteReadListAsync(string query, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default(CancellationToken));
    Task<List<Dictionary<string, object>>> ExecuteReadDictionaryAsync(string query, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default(CancellationToken));
    Task<T> ExecuteReadScalarAsync<T>(string query, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default(CancellationToken));
    Task<T> ExecuteWriteTransactionAsync<T>(string query, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default(CancellationToken));
    Task<List<T>> ExecuteReadListAsync<T>(string query, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default(CancellationToken));
}
