using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

        // GET: api/Componenti
        // [AllowAnonymous]
        /*
        public IQueryable<Componente> GetTblComponenti()
        {
            return db.TblComponenti;
        }
        */
        public IHttpActionResult GetTblComponenti()
        {
            var result = db.TblComponenti.Select(t => new
            {
                t.Codice,
                t.Descrizione,
                t.Note,
                t.ComponenteID,
                t.CarrelloID
            });
            return Ok(result.ToList());
        }
        
        // GET: api/Componenti/5
        [ResponseType(typeof(Componente))]
        public IHttpActionResult GetComponente(int id)
        {
            Componente componente = db.TblComponenti.Find(id);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Componenti
        [ResponseType(typeof(Componente))]
        public IHttpActionResult PostComponente(Componente componente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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