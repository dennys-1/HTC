using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_santa_ursula_II.Models
{
   [Table("T_actividades")]
    public class Actividades
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        
        [Display(Name="Código:")]
        public string codigo { get; set; }

        [Display(Name="Precio:")]
        public int precio { get; set; }

        [Display(Name="Descripción:")]
        public string descripcion { get; set; }

        
        
        [Display(Name="Estado:")]
        public string Estado { get; set; }

        [Display(Name="Imagen:")]
        public string Imagen { get; set; }
        
        [Display(Name="Tipo:")]
        public TipoActividad tipoActividad { get; set; }

    }
}