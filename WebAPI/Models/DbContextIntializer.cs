using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class DbContextIntializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            Carrello carrello1 = new Carrello("CR46281937823", DateTime.Today.Year, "AREA STOCK AS1", "LOCAZIONE MD3");
            Carrello carrello2 = new Carrello("CR01253232548", DateTime.Today.Year, "AREA STOCK AS2", "LOCAZIONE MD2");
            context.TblCarrelli.Add(carrello1);
            context.TblCarrelli.Add(carrello2);

            Componente componente1 = new Componente("BT1000", "Batteria 1000", "", 1);
            context.TblComponenti.Add(componente1);
            Componente componente2 = new Componente("MTH30", "Montante H30", "Montante utilizzato in carrelli di colori giallo e nero", 1);
            context.TblComponenti.Add(componente2);
            Componente componente3 = new Componente("FR343", "Forca 343", "Forca allungabile", 1);
            context.TblComponenti.Add(componente3);
            Componente componente4 = new Componente("FR343", "Forca 343", "Forca allungabile", 1);
            context.TblComponenti.Add(componente4);
            Componente componente5 = new Componente("BT2000", "Batteria 2000", "Maneggiare con cura e con l'utilizzo di guanti", 2);
            context.TblComponenti.Add(componente5);
            Componente componente6 = new Componente("MTH30", "Montante H30", "Montante utilizzato in carrelli di colori giallo e nero", 2);
            context.TblComponenti.Add(componente6);

            context.SaveChanges();
            base.Seed(context);
        }
    }
}