using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominioWebApp.DTOs
{
    public class TipoGastoDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del tipo de gasto es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La descripcion del tipo de gasto es requerida")]
        public string Descripcion { get; set; }
    }
}
