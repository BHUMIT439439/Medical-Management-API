using MedicalStoreWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MedicalStoreWebAPI.Controllers
{
    public class MedicineMVCController : Controller
    {
        MedicalDatabaseEntities db = new MedicalDatabaseEntities();
        HttpClient client = new HttpClient();

        // GET: MedicineMVC
        public ActionResult Index()
        {
            List<Medicine> medList = new List<Medicine>();
            client.BaseAddress = new Uri("https://localhost:44381/api/MedicineAPI");
            var response = client.GetAsync("MedicineAPI");
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<Medicine>>();
                display.Wait();
                medList = display.Result;
            }

            return View(medList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Medicine med)
        {
            List<Medicine> medList = new List<Medicine>();
            client.BaseAddress = new Uri("https://localhost:44381/api/MedicineAPI");
            var response = client.PostAsJsonAsync<Medicine>("MedicineAPI", med);
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Create");
            }

        }

        public ActionResult Edit(int id)
        {
            Medicine m = null;
            client.BaseAddress = new Uri("https://localhost:44381/api/MedicineAPI");
            var response = client.GetAsync("MedicineAPI?id=" + id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Medicine>();
                display.Wait();
                m = display.Result;
            }
            return View(m);
        }

        [HttpPost]
        public ActionResult Edit(Medicine med)
        {
            List<Medicine> medList = new List<Medicine>();
            client.BaseAddress = new Uri("https://localhost:44381/api/MedicineAPI");
            var response = client.PutAsJsonAsync<Medicine>("MedicineAPI", med);
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Edited successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Edit");
            }
        }

        public ActionResult Delete(int id)
        {
            Medicine m = null;
            client.BaseAddress = new Uri("https://localhost:44381/api/MedicineAPI");
            var response = client.GetAsync("MedicineAPI?id=" + id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Medicine>();
                display.Wait();
                m = display.Result;
            }
            return View(m);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            client.BaseAddress = new Uri("https://localhost:44381/api/MedicineAPI");
            var response = client.DeleteAsync("MedicineAPI/" + id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Deleted successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Delete");
            }
        }

        public ActionResult Buy(int id)
        {
            Medicine m = null;
            client.BaseAddress = new Uri("https://localhost:44381/api/MedicineAPI");
            var response = client.GetAsync("MedicineAPI?id=" + id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Medicine>();
                display.Wait();
                m = display.Result;
            }
            return View(m);
        }
       
        [HttpPost]
        public ActionResult Buy(Medicine med)
        {
            List<Medicine> medList = new List<Medicine>();
            int qty = med.Srock;
            Medicine data = db.Medicines.Where(e => e.Name == med.Name).FirstOrDefault();
            if (data != null)
            {
                data.Srock = data.Srock - qty;
                db.SaveChanges();
            }

            client.BaseAddress = new Uri("https://localhost:44381/api/PurchasesAPI");
            var response = client.PostAsJsonAsync<Medicine>("PurchasesAPI", med);
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Purchased successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Buy");
            }
        }
        public ActionResult History()
        {
            List<Purchase> purchaseList = new List<Purchase>();
            client.BaseAddress = new Uri("https://localhost:44381/api/PurchasesAPI");
            var response = client.GetAsync("PurchasesAPI");
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<Purchase>>();
                display.Wait();
                purchaseList = display.Result;
            }

            return View(purchaseList);



        }
    }
}