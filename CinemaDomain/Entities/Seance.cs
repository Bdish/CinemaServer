using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaDomain.Entities
{
    /// <summary>
    /// Киносеансы
    /// </summary>
    public class Seance
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
       // [MaxLength(250)]
        public string Name { get; set; }
       
        [Required]
        public DateTime Start { get; set; }//время начала киносеанса

    }
}
