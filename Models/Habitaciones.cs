using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_santa_ursula_II.Models
{
    [Table("T_habitaciones")]
    public class Habitaciones
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        
        [Display(Name="Número:")]
        public string numero { get; set; }

        [Display(Name="Nombre:")]
        public string Nomhab { get; set;}

        [Display(Name="Precio:")]
        public int price { get; set; }

        [Display(Name="Descripción:")]
        public string descrip { get; set; }

        [Display(Name="Nivel:")]
        public int nivel { get; set; }
        
        [Display(Name="Estado:")]
        public string Status { get; set; }

        [Display(Name="Imagen:")]
        public string Imagen { get; set; }
        
        [Display(Name="Tipo:")]
        public TipoHabitacion tipoHabitacion { get; set; }

    }
}