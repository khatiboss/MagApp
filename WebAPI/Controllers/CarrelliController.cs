﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize(Users ="Admin")]
    public class CarrelliController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Carrelli
        [AllowAnonymous]
        public IQueryable<Carrello> GetTblCarrelli()
        {
            return db.TblCarrelli;
        }

        // GET: api/Carrelli/5
        [ResponseType(typeof(Carrello))]
        public IHttpActionResult GetCarrello(int id)
        {
            Carrello carrello = db.TblCarrelli.Find(id);
            if (carrello == null)
            {
                return NotFound();
            }

            return Ok(carrello);
        }

        // PUT: api/Carrelli/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCarrello(int id, Carrello carrello)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != carrello.CarrelloID)
            {
                return BadRequest();
            }

            db.Entry(carrello).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarrelloExists(id))
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

        // POST: api/Carrelli
        [ResponseType(typeof(Carrello))]
        public IHttpActionResult PostCarrello(Carrello carrello)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TblCarrelli.Add(carrello);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = carrello.CarrelloID }, carrello);
        }

        // DELETE: api/Carrelli/5
        [ResponseType(typeof(Carrello))]
        public IHttpActionResult DeleteCarrello(int id)
        {
            Carrello carrello = db.TblCarrelli.Find(id);
            if (carrello == null)
            {
                return NotFound();
            }

            db.TblCarrelli.Remove(carrello);
            db.SaveChanges();

            return Ok(carrello);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarrelloExists(int id)
        {
            return db.TblCarrelli.Count(e => e.CarrelloID == id) > 0;
        }
    }
}