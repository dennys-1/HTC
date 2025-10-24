using System;
using System.ComponentModel.DataAnnotations;

namespace hotel_santa_ursula_II.Models
{
    public class Driver
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [MaxLength(80)]
        public string VehicleModel { get; set; }

        [MaxLength(12)]
        public string LicensePlate { get; set; }

        [Range(0, 5)]
        public double Rating { get; set; } = 5;

        public bool IsAvailable { get; set; } = true;

        [Range(-90, 90)]
        public double? CurrentLatitude { get; set; }

        [Range(-180, 180)]
        public double? CurrentLongitude { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
