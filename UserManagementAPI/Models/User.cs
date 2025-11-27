using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class User //Repersents a user in the system 
    {
        [Required]
        public required int Id { get; set; }

        [StringLength(100)]
        public required string name { get; set; }

        [EmailAddress]
        public required string Email { get; set; }
        public int? Age { get; set; }
    }
}