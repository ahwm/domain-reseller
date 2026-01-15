using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainReseller.Data
{
    [Table(nameof(OpenSRSPricing))]
    [PrimaryKey(nameof(Id))]
    public class OpenSRSPricing
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string TLD { get; set; } = "";

        [Column(TypeName = "decimal(18,2)")]
        public decimal RegistrationPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TransferPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal RenewalPrice { get; set; }
    }
}
