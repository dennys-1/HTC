using System.ComponentModel.DataAnnotations;

namespace hotel_santa_ursula_II.Models
{
    public class Rider
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [MaxLength(80)]
        public string PreferredPaymentMethod { get; set; }

        [EmailAddress]
        [MaxLength(120)]
        public string Email { get; set; }
    }
}
