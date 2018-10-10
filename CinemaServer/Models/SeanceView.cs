using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class SeanceView
    {       
        public int? Id { get; set; }

        [Required(ErrorMessage = "Введите название фильма.")]
        public string Name { get; set; }

       // [JsonConverter(typeof(DateTimeConverter))]
        [Required(ErrorMessage = "Укажите дату и время")]
        public DateTime? Start { get; set; }//время начала киносеанса
    }
}
