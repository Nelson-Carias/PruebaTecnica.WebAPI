using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.EntidadesDeNegocio
{
    public class Vehiculo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre es obligatorio")]
        [StringLength(60, ErrorMessage = "Maximo 60 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Año es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Año { get; set; }

        [Required(ErrorMessage = "Descripcion es obligatorio")]
        //[MaxLength(6000, ErrorMessage = "Máximo 6000 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Precio es obligatorio")]
        [StringLength(100, ErrorMessage = "Maximo 100 caracteres")]
        public string Precio { get; set; }

        [Required(ErrorMessage = "Imagen es obligatoria")]
        //[MaxLength(2000, ErrorMessage = "Máximo 2000 caracteres")]
        public string Imagen { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }
    }
}
