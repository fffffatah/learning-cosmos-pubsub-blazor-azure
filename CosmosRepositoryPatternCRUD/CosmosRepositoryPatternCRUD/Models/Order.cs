using Microsoft.Azure.CosmosRepository;
using Microsoft.Azure.CosmosRepository.Attributes;
using System.ComponentModel.DataAnnotations;

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
    [PartitionKeyPath("/id")]
    public class Order : Item
    {
        [Required]
        public DateTime PlacedAt { get; set; }
        [Required]
        public OrderStatuses Status { get; set; }
        [Required]
        public User? OrderedBy { get; set; }
        [Required]
        public List<Book>? Books { get; set; }
    }
}
