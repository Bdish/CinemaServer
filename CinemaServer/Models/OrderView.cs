using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class OrderView
    {
        
        public int? Id { get; set; }

        [Required(ErrorMessage = "Укажите сеанс.")]
        public int IdSeance { get; set; }

        [Required(ErrorMessage = "Введите кл-во мест.")]
        public int CountPlace { get; set; }
    }
}
