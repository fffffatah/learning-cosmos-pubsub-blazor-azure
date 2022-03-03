using Microsoft.Azure.CosmosRepository;
using Microsoft.Azure.CosmosRepository.Attributes;

namespace CosmosRepositoryPatternCRUD.Models
{
    public enum OrderStatuses
    {
        Processing,
        Processed,
        Shipping,
        Delivered
    }
    [Container("orders")]
    [PartitionKeyPath("/placed_at")]
    public class Order : Item
    {
        public DateTime PlacedAt { get; set; }
        public OrderStatuses Status { get; set; }
        public User? OrderedBy { get; set; }
        public List<Book>? Books { get; set; }
        protected override string GetPartitionKeyValue() => PlacedAt.ToString();
    }
}
