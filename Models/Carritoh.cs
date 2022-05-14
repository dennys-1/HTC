using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_santa_ursula_II.Models
{
    [Table("t_proformah")]
    public class Carritoh
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]

        public int Id {get; set;}

        public String UserID { get; set;}

        public Habitaciones Prodhab { get; set;}

        public string numero { get; set;}

        public string Nomhab { get; set;}

        public int Quantity{get; set;}

        public int price {get; set;}

        public int C_noches {get; set;} =1;

        public String Status { get; set; } = "PENDIENTE";

        public DateTime fechar{get; set;}
    }
}