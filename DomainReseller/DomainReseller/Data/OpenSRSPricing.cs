using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainReseller.Data
{
    [Table(nameof(OpenSRSPricing))]
    [PrimaryKey(nameof(Id))]
    public class OpenSRSPricing
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string TLD { get; set; } = "";

        public decimal RegistrationPrice { get; set; }

        public decimal TransferPrice { get; set; }

        public decimal RenewalPrice { get; set; }
    }
}
