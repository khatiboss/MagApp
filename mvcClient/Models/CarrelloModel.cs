using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvcClient.Models
{
    public class CarrelloModel
    {


        public int CarrelloID { get; set; }

        [Required(ErrorMessage = "Campo Matricola è obbligatorio")]
        public string Matricola { get; set; }

        [Required(ErrorMessage = "Campo Anno Arrivo è obbligatorio")]
        [Range(1900, 2100, ErrorMessage = "Questo campo deve essere con formato di Anno es. 2019")]
        public int AnnoArrivo { get; set; }

        [Required(ErrorMessage = "Campo Area Stock è obbligatorio")]
        public string AreaStock { get; set; }

        [Required(ErrorMessage = "Campo Locazione è obbligatorio")]
        public string Locazione { get; set; }

        public virtual ICollection<ComponenteModel> Componenti { get; set; }

        public CarrelloModel()
        {
        }

        public CarrelloModel(string matricola, int annoArrivo, string areaStock, string locazione)
        {

            this.Matricola = matricola;
            this.AnnoArrivo = annoArrivo;
            this.AreaStock = areaStock;
            this.Locazione = locazione;
        }

        public CarrelloModel(string matricola, int annoArrivo, string areaStock, string locazione, List<ComponenteModel> listaComponenti)
        {

            this.Matricola = matricola;
            this.AnnoArrivo = annoArrivo;
            this.AreaStock = areaStock;
            this.Locazione = locazione;
            this.Componenti = listaComponenti;
        }

    }
}