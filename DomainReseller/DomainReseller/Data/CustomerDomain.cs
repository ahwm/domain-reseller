using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainReseller.Data
{
    [Table(nameof(CustomerDomain))]
    [PrimaryKey(nameof(Id))]
    public class CustomerDomain
    {
        [Key,Required]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = "";

        [Required]
        public string DomainName { get; set; } = "";
    }
}
