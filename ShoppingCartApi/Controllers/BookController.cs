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

namespace ShoppingCartApi.Controllers
{
    public class BookController : ApiController
    {
        //private BookEntities db = new BookEntities();

        //// GET: api/Book
        //public IQueryable<TRBKHDR2> GetTRBKHDR2()
        //{
        //    return db.TRBKHDR2;
        //}

        //// GET: api/Book/5
        //[ResponseType(typeof(TRBKHDR2))]
        //public IHttpActionResult GetTRBKHDR2(string id)
        //{
        //    var tRBKHDR2 = db.TRBKBIB2.Where(r => r.BARCODE == id).FirstOrDefault();
        //    //TRBKHDR2 tRBKHDR2 = db.TRBKHDR2.Find(id);

        //    if (tRBKHDR2 == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tRBKHDR2);
        //}

        //// PUT: api/Book/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutTRBKHDR2(string id, TRBKHDR2 tRBKHDR2)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != tRBKHDR2.ISBN)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(tRBKHDR2).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TRBKHDR2Exists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Book
        //[ResponseType(typeof(TRBKHDR2))]
        //public IHttpActionResult PostTRBKHDR2(TRBKHDR2 tRBKHDR2)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.TRBKHDR2.Add(tRBKHDR2);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (TRBKHDR2Exists(tRBKHDR2.ISBN))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = tRBKHDR2.ISBN }, tRBKHDR2);
        //}

        //// DELETE: api/Book/5
        //[ResponseType(typeof(TRBKHDR2))]
        //public IHttpActionResult DeleteTRBKHDR2(string id)
        //{
        //    TRBKHDR2 tRBKHDR2 = db.TRBKHDR2.Find(id);
        //    if (tRBKHDR2 == null)
        //    {
        //        return NotFound();
        //    }

        //    db.TRBKHDR2.Remove(tRBKHDR2);
        //    db.SaveChanges();

        //    return Ok(tRBKHDR2);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TRBKHDR2Exists(string id)
        {
            return false; // db.TRBKHDR2.Count(e => e.ISBN == id) > 0;
        }
    }
}