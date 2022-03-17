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
using MedicalStoreWebAPI.Models;

namespace MedicalStoreWebAPI.Controllers
{
    public class PurchasesAPIController : ApiController
    {
        private MedicalDatabasePurchaseEntities db = new MedicalDatabasePurchaseEntities();

        // GET: api/PurchasesAPI
        public IQueryable<Purchase> GetPurchases()
        {
            return db.Purchases;
        }

        // GET: api/PurchasesAPI/5
        [ResponseType(typeof(Purchase))]
        public IHttpActionResult GetPurchase(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return NotFound();
            }

            return Ok(purchase);
        }

        // PUT: api/PurchasesAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchase(int id, Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchase.Id)
            {
                return BadRequest();
            }

            db.Entry(purchase).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseExists(id))
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

        // POST: api/PurchasesAPI
        [ResponseType(typeof(Purchase))]
        public IHttpActionResult PostPurchase(Medicine m)
        {
            Purchase p = new Purchase();

            p.Id = m.MedicineID;
            p.MedicineName = m.Name;
            p.MedicineCompanyName = m.CompanyName;
            p.MedicinePrice = m.Price;
            p.MedicineQuantity = m.Srock;
            p.Amount = m.Price * m.Srock;
            p.Date = DateTime.Now.Date;

            db.Purchases.Add(p);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = p.Id }, p);
        }

        // DELETE: api/PurchasesAPI/5
        [ResponseType(typeof(Purchase))]
        public IHttpActionResult DeletePurchase(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return NotFound();
            }

            db.Purchases.Remove(purchase);
            db.SaveChanges();

            return Ok(purchase);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PurchaseExists(int id)
        {
            return db.Purchases.Count(e => e.Id == id) > 0;
        }
    }
}