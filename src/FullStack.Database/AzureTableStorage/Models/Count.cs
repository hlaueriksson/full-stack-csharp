using Microsoft.Azure.Cosmos.Table;

namespace FullStack.Database.AzureTableStorage.Models
{
    public class Count : TableEntity
    {
        public static readonly Count Default = new Count();

        public int Value { get; set; }

        public Count()
        {
            PartitionKey = "Count";
            RowKey = "Count";
        }
    }
}