using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.EntidadesDeNegocio
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre es obligatorio")]
        [StringLength(25, ErrorMessage = "Máximo 25 caracteres")]
        public string Nombre { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }

        [ValidateNever]
        public List<Usuario> Usuario { get; set; }
    }
}
