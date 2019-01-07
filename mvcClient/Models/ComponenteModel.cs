using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvcClient.Models
{
    public class ComponenteModel
    {

        public int ComponenteID { get; set; }

        [Required(ErrorMessage = "Campo Codice è obbligatorio")]
        [StringLength(10,ErrorMessage = "Lunghezza Massima 10 caratteri")]
        public string Codice { get; set; }

        [Required(ErrorMessage = "La Descrizione è obbligatorio"), MinLength(5,ErrorMessage ="Min 5 Char")]
        public string Descrizione { get; set; }

        public string Note { get; set; }

        public int CarrelloID { get; set; }


        public virtual CarrelloModel Carrello { get; set; }

        public ComponenteModel()
        {
        }

        public ComponenteModel(string codice, string descrizione, string note, int carrelloID)
        {
            Codice = codice;
            Descrizione = descrizione;
            Note = note;
            CarrelloID = carrelloID;
        }

        public ComponenteModel(string codice, string descrizione, string note, int carrelloID, CarrelloModel carrello)
        {
            Codice = codice;
            Descrizione = descrizione;
            Note = note;
            CarrelloID = carrelloID;
            Carrello = carrello;
        }
    }
}