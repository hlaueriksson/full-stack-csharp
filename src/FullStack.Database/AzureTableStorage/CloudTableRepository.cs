using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace FullStack.Database.AzureTableStorage
{
    public interface ICloudTableRepository<T> where T : TableEntity
    {
        Task<T> DeleteAsync(T entity);
        Task<T> InsertAsync(T entity);
        Task<T> InsertOrMergeAsync(T entity);
        Task<T> InsertOrReplaceAsync(T entity);
        Task<T> MergeAsync(T entity);
        Task<T> ReplaceAsync(T entity);
        Task<T> RetrieveAsync(T entity);
        Task<T> RetrieveAsync(string partitionKey, string rowKey);
    }

    public class CloudTableRepository<T> : ICloudTableRepository<T> where T : TableEntity, new()
    {
        private readonly string _tableName = typeof(T).Name;

        protected readonly CloudTable Table;

        public CloudTableRepository(string connectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var tableClient = storageAccount.CreateCloudTableClient();
            Table = tableClient.GetTableReference(_tableName);
            Table.CreateIfNotExists();
        }

        public virtual async Task<T> DeleteAsync(T entity)
        {
            return await GetResult(TableOperation.Delete(entity));
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            return await GetResult(TableOperation.Insert(entity));
        }

        public virtual async Task<T> InsertOrMergeAsync(T entity)
        {
            return await GetResult(TableOperation.InsertOrMerge(entity));
        }

        public virtual async Task<T> InsertOrReplaceAsync(T entity)
        {
            return await GetResult(TableOperation.InsertOrReplace(entity));
        }

        public virtual async Task<T> MergeAsync(T entity)
        {
            return await GetResult(TableOperation.Merge(entity));
        }

        public virtual async Task<T> ReplaceAsync(T entity)
        {
            return await GetResult(TableOperation.Replace(entity));
        }

        public virtual async Task<T> RetrieveAsync(T entity)
        {
            return await RetrieveAsync(entity.PartitionKey, entity.RowKey);
        }

        public virtual async Task<T> RetrieveAsync(string partitionKey, string rowKey)
        {
            return await GetResult(TableOperation.Retrieve<T>(partitionKey, rowKey));
        }

        private async Task<T> GetResult(TableOperation delete)
        {
            var result = await Table.ExecuteAsync(delete);

            return IsSuccessStatusCode(result) ? result.Result as T : null;
        }

        private bool IsSuccessStatusCode(TableResult result)
        {
            return result.HttpStatusCode >= 200 && result.HttpStatusCode <= 299;
        }
    }
}