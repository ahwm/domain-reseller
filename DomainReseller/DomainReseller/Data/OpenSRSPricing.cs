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
        public string TLD { get; set; } = "";

        [Required]
        public decimal RegistrationPrice { get; set; }

        [Required]
        public decimal TransferPrice { get; set; }

        [Required]
        public decimal RenewalPrice { get; set; }
    }
}
