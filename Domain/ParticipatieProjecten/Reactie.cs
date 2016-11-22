using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BEP.BL.Models.ParticipatieProjecten
{
    public class Reactie
    {
        public int ReactieId { get; set; }
        [Required(ErrorMessage = "Een reactie moet een userid meekrijgen")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Een reactie moet tekst bevatten")]
        [StringLength(1000, ErrorMessage = "Een reactie mag niet meer dan 1000 karakters bevatten")]
        public string Tekst { get; set; }
        [Required(ErrorMessage = "De datum moet ingevuld zijn")]
        public DateTime Datum { get; set; }
        public int Draagvlak { get; set; }
        public ICollection<Reactie> SubReacties { get; set; }
        public ICollection<ReactieLike> Likes { get; set; }

        public Reactie()
        {
            SubReacties = new List<Reactie>();
            Likes = new List<ReactieLike>();
        }

    }
}
