using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_santa_ursula_II.Models
{
    [Table("T_tipo_actividad")]    
    public class TipoActividad
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un nombre para el tipo de actividad")]   
        [Display(Name="Nombre del tipo de actividad: ")]
        [Column("nomtipactividad")]
        public string nomtipactividad { get; set; }
        
        [Required(ErrorMessage = "Por favor ingrese una descripción")]   
        [Display(Name="Descripción: ")]
        [Column("desctipact")]
        public string desctipact { get; set; }

      
    }
}