using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_santa_ursula_II.Models
{
    public class RideRequest
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Rider))]
        public int RiderId { get; set; }

        public Rider Rider { get; set; }

        [ForeignKey(nameof(Driver))]
        public int? DriverId { get; set; }

        public Driver Driver { get; set; }

        [Required]
        [MaxLength(180)]
        public string PickupAddress { get; set; }

        [Required]
        [MaxLength(180)]
        public string DropoffAddress { get; set; }

        [Range(-90, 90)]
        public double PickupLatitude { get; set; }

        [Range(-180, 180)]
        public double PickupLongitude { get; set; }

        [Range(-90, 90)]
        public double DropoffLatitude { get; set; }

        [Range(-180, 180)]
        public double DropoffLongitude { get; set; }

        [Range(0, 10000)]
        public decimal FareEstimate { get; set; }

        public RideStatus Status { get; set; } = RideStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? AcceptedAt { get; set; }

        public DateTime? CompletedAt { get; set; }
    }
}
