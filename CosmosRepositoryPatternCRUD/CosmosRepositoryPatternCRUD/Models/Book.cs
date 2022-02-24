using Microsoft.Azure.CosmosRepository;

namespace CosmosRepositoryPatternCRUD.Models
{
    public class Book : Item
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
    }
}
