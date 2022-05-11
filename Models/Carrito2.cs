using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_santa_ursula_II.Models
{
    [Table("t_proforma2")]
    public class Carrito2
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id {get; set;}

        public String UserID { get; set;}

        public Actividades Producto { get; set;}

        public string codigo { get; set;}

        public string nombre { get; set;}

        public int precio { get; set;}
        

        public String Status { get; set;} = "PENDIENTE";
    }
    }
