using Microsoft.Azure.CosmosRepository;
using Microsoft.Azure.CosmosRepository.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CosmosRepositoryPatternCRUD.Models
{
    [Container("users")]
    [PartitionKeyPath("/email")]
    public class User : Item
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Address { get; set; }
        protected override string GetPartitionKeyValue() => Email;
    }
}
