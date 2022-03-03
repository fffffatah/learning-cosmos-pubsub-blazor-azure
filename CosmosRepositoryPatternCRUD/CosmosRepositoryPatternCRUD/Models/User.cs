using Microsoft.Azure.CosmosRepository;
using Microsoft.Azure.CosmosRepository.Attributes;

namespace CosmosRepositoryPatternCRUD.Models
{
    [Container("users")]
    [PartitionKeyPath("/username")]
    public class User : Item
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        protected override string GetPartitionKeyValue() => UserName;
    }
}
