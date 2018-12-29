using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    [Table("TblComponenti")]
    public class Componente
    {
        [Key] 
        [Display(Name = "ID Componente")]
        public int ComponenteID { get; set; }

        [Required]
        [StringLength(10)]
        public string Codice { get; set; }

        [Required, MinLength(5), MaxLength(70)]
        public string Descrizione { get; set; }

        public string Note { get; set; }

        public int CarrelloID { get; set; }
       
        [ForeignKey("CarrelloID")]
        public virtual Carrello Carrello { get; set; }

        public Componente()
        {
        }

        public Componente(string codice, string descrizione, string note, int carrelloID)
        {
            Codice = codice;
            Descrizione = descrizione;
            Note = note;
            CarrelloID = carrelloID;
        }
    }
}