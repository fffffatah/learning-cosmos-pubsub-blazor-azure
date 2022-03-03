using Microsoft.Azure.CosmosRepository.Attributes;
using Microsoft.Azure.CosmosRepository;
using System.ComponentModel.DataAnnotations;

namespace CosmosRepositoryPatternCRUD.Models
{
    [Container("books")]
    [PartitionKeyPath("/genre")]
    public class Book : Item
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Author { get; set; }
        [Required]
        public string? Genre { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
        /// <summary>
        /// Items having same partition key value, in this case 'Genre'
        /// will be grouped together in the same logical partition.
        /// Each logical partition can store upto 20GB of data.
        /// </summary>
        protected override string GetPartitionKeyValue() => Genre;
    }
}
