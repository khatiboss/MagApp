using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    [Table("TblCarrelli")]
    public class Carrello
    {
        [Key]
        [Display(Name = "ID Carrello")]
        public int CarrelloID { get; set; }

        [Required]
        public string Matricola { get; set; }

        [Required, Range(1900, 2100)]
        public int AnnoArrivo { get; set; }

        [Required, StringLength(100)]
        public string AreaStock { get; set; }
        [Required, StringLength(100)]
        public string Locazione { get; set; }

 
        public virtual ICollection<Componente> Componenti { get; set; }


        public Carrello(string matricola, int annoArrivo, string areaStock, string locazione)
        {

            this.Matricola = matricola;
            this.AnnoArrivo = annoArrivo;
            this.AreaStock = areaStock;
            this.Locazione = locazione;
        }

    }
}