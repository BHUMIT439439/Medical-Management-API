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
                return RedirectToAction("Index");
            }
            else
            {
                return View("Delete");
            }
        }
    }
}