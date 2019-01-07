using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    //[Authorize(Users ="Admin")]
    [EnableCors(origins: "http://localhost:50312", headers: "*", methods: "*")]
    public class ComponentiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public IHttpActionResult GetTblComponenti()
        {
            /*
            var result = db.TblComponenti.Select(t => new
            {
                t.ComponenteID,
                t.CarrelloID,
                t.Codice,
                t.Descrizione,
                t.Note,
                Matricola=t.Carrello.Matricola,
                AnnoArrivo=t.Carrello.AnnoArrivo,
                AreaStock=t.Carrello.AreaStock,
                Locazione=t.Carrello.Locazione,

            });
            */

            var result =
            from cm in db.TblComponenti
            group cm by cm.Codice into cd
            select new
            {
                Codice = cd.Key,
                Quantita = cd.Count(),
                //Descrizione=cd.Select(cm =>cm.Descrizione),
                Carrelli = cd.Select(cm => new
                {
                    cm.Carrello.Matricola,
                    cm.Carrello.AreaStock,
                    cm.Carrello.Locazione,
                    cm.Carrello.AnnoArrivo,
                    cm.Carrello.CarrelloID
                }

            )
            };

            return Ok(result.ToList());
        }

        // GET: api/Componenti/5
        [ResponseType(typeof(Componente))]
        public IHttpActionResult GetComponente(int id)
        {
            var componente = db.TblComponenti.Where(cm => cm.ComponenteID == id).Select(cm => new
            {
                cm.ComponenteID,
                cm.Codice,
                cm.Descrizione,
                cm.Note,
                cm.CarrelloID,

                Matricola = cm.Carrello.Matricola,
                AreaStock = cm.Carrello.AreaStock,
                Locazione = cm.Carrello.Locazione,
                AnnoArrivo = cm.Carrello.AnnoArrivo,




            }).FirstOrDefault();


            if (componente == null)
            {
                return NotFound();
            }

            return Ok(componente);
        }

        // PUT: api/Componenti/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComponente(int id, Componente componente)
        {


            if (id != componente.ComponenteID)
            {
                return BadRequest();
            }

            db.Entry(componente).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponenteExists(id))
                {
                    return NotFound();
                }




            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Componenti
        [ResponseType(typeof(Componente))]
        public IHttpActionResult PostComponente(Componente componente)
        {


            db.TblComponenti.Add(componente);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = componente.ComponenteID }, componente);


        }

        // DELETE: api/Componenti/5
        [ResponseType(typeof(Componente))]
        public IHttpActionResult DeleteComponente(int id)
        {
            Componente componente = db.TblComponenti.Find(id);
            if (componente == null)
            {
                return NotFound();
            }

            db.TblComponenti.Remove(componente);
            db.SaveChanges();

            return Ok(componente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComponenteExists(int id)
        {
            return db.TblComponenti.Count(e => e.ComponenteID == id) > 0;
        }
    }
}