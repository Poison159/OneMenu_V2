using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebOneApp.Models;

namespace WebOneApp.Controllers.API
{
    public class ResturantsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Resturants
        public IQueryable<Resturant> GetResturants()
        {
            return db.Resturants;
        }

        // GET: api/Resturants/5
        [ResponseType(typeof(Resturant))]
        public object GetResturant(string code)
        {
            try
            {
                Resturant resturant = db.Resturants.ToList().First(x => x.guid == code);
                Helper.SortMeals(db.Meals.Where(x => x.resturantId == resturant.id).ToList(), resturant.meals);
                if (resturant == null){
                    return NotFound();
                }

                return Ok(resturant);
            }catch(Exception) {
                return new { Erros = "Could not find resturant" };
            }
            
        }

        // PUT: api/Resturants/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutResturant(int id, Resturant resturant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != resturant.id)
            {
                return BadRequest();
            }

            db.Entry(resturant).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResturantExists(id))
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

        // POST: api/Resturants
        [ResponseType(typeof(Resturant))]
        public IHttpActionResult PostResturant(Resturant resturant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Resturants.Add(resturant);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = resturant.id }, resturant);
        }

        // DELETE: api/Resturants/5
        [ResponseType(typeof(Resturant))]
        public IHttpActionResult DeleteResturant(int id)
        {
            Resturant resturant = db.Resturants.Find(id);
            if (resturant == null)
            {
                return NotFound();
            }

            db.Resturants.Remove(resturant);
            db.SaveChanges();

            return Ok(resturant);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResturantExists(int id)
        {
            return db.Resturants.Count(e => e.id == id) > 0;
        }
    }
}