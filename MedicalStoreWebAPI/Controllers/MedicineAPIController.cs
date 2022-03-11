using MedicalStoreWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MedicalStoreWebAPI.Controllers
{
    public class MedicineAPIController : ApiController
    {
        MedicalDatabaseEntities db = new MedicalDatabaseEntities();

        // GET: MedicineAPI 
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetMedicines()
        {
            List<Medicine> medicinesList = db.Medicines.ToList();
            return Ok(medicinesList);
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetMedicine(int id)
        {
            var medicine = db.Medicines.Where(model => model.MedicineID == id).FirstOrDefault();
            return Ok(medicine);
        }

        //[System.Web.Http.HttpGet]
        //public IHttpActionResult GetMedicineById(int id)
        //{
        //    var medicine = db.Medicines.Where(model => model.MedicineID == id).FirstOrDefault();
        //    return Ok(medicine);
        //}

        [System.Web.Http.HttpPost]
        public IHttpActionResult AddMedicine(Medicine m)
        {
            db.Medicines.Add(m);
            db.SaveChanges();
            return Ok();
        }

        [System.Web.Http.HttpPut]
        public IHttpActionResult UpdateMedicine(Medicine m)
        {
            var med = db.Medicines.Where(model => model.MedicineID == m.MedicineID).FirstOrDefault();
            if(med != null)
            {
                med.MedicineID = m.MedicineID;
                med.Name = m.Name;
                med.CompanyName = m.CompanyName;
                med.Srock = m.Srock;
                med.Price = m.Price;
                db.SaveChanges();
            }
            else
            {
                NotFound();
            }
            return Ok();
        }

        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteMedicine(int id)
        {
            var med = db.Medicines.Where(model => model.MedicineID == id).FirstOrDefault();
            db.Entry(med).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return Ok();
        }
    }
}