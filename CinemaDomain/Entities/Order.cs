using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CinemaDomain.Entities
{
    /// <summary>
    /// Заказ на покупку билетов
    /// </summary>
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int IdSeance { get; set; }//на какой киносеанс

        [Required]
        public int CountPlace { get; set; }//сколько мест

        [Required]
        public DateTime TicketSales { get; set; } = DateTime.Now;
    }
}
