using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using API.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.ProjectModel;
using UI.Data;
using UI.Models;
using UI.ViewModels.Doctors;
using UI.ViewModels.Medications;
using X.PagedList;

namespace UI.Controllers
{
    public class MedicationsController : Controller
    {
        private readonly UIContext _context;

        public MedicationsController(UIContext context)
        {
            _context = context;
        }

        // GET: Medications
        public async Task<IActionResult> Index(MIndexVM model, int? page, string? name)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7269/");
                client.DefaultRequestHeaders.Add(AuthConstants.ApiKeyHeaderName, AuthConstants.ApiKeyHeaderValue);
                try
                {
                    response = client.GetAsync(client.BaseAddress
                       + "api/Medications/").Result;
                }
                catch (Exception)
                {

                    throw;
                }
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    model.Items = JsonConvert.DeserializeObject<List<MedicationVM>>(result);
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            if (name != null)
            {
                model.Items = model.Items.Where(x => x.Name.ToLower() == name.ToLower()).ToList();
            }
            int pageNumber = page ?? 1;
            int pageSize = 10;
            var pagedDoctors = model.Items.ToPagedList(pageNumber, pageSize);
            return View(pagedDoctors);
        }

        // GET: Medications/Details/5
        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            MedicationVM model = new MedicationVM();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7269/");
                client.DefaultRequestHeaders.Add(AuthConstants.ApiKeyHeaderName, AuthConstants.ApiKeyHeaderValue);
                response = client.GetAsync(client.BaseAddress
                   + "api/Medications/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<MedicationVM>(result);
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(model);
        }
        

        // GET: Medications/Create
        public IActionResult Create(int id)
        {
            MCreateVM model = new MCreateVM();
            model.Id = id;
            return View(model);
        }

        // POST: Medications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MCreateVM model)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = new HttpResponseMessage();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7269/");
                    client.DefaultRequestHeaders.Add(AuthConstants.ApiKeyHeaderName, AuthConstants.ApiKeyHeaderValue);
                    string data = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent(data, Encoding.UTF8,
                        "application/json");
                    response = client.PostAsync(client.BaseAddress
                        + "api/Medications", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        data = response.Content.ReadAsStringAsync().Result;
                        JObject result = JObject.Parse(data);
                    }


                    return RedirectToAction("Index");

                }
            }
            return View(model);
        }

        // GET: Medications/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            MCreateVM model = new MCreateVM();
            HttpResponseMessage response = new HttpResponseMessage();
            JObject data = new JObject();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7269/");
                client.DefaultRequestHeaders.Add(AuthConstants.ApiKeyHeaderName, AuthConstants.ApiKeyHeaderValue);
                response = client.GetAsync(client.BaseAddress
                    + "api/Medications/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    data = JObject.Parse(result);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            model.Id = data.GetValue<int>("id");
            model.Name = data.GetValue<string>("name");
            model.Manufacturer = data.GetValue<string>("manufacturer");
            model.Type = data.GetValue<string>("type");
            model.Dosage = data.GetValue<string>("dosage");
            model.Price = data.GetValue<decimal>("price");
            model.ExpirationDate = data.GetValue<DateTime>("expirationDate");


            return View(model);
        }

        // POST: Medications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MCreateVM model)
        {
            if (ModelState.IsValid)
            {

                HttpResponseMessage response = new HttpResponseMessage();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7269/");
                    client.DefaultRequestHeaders.Add(AuthConstants.ApiKeyHeaderName, AuthConstants.ApiKeyHeaderValue);
                    string data = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent(data, Encoding.UTF8,
                        "application/json");
                    response = client.PutAsync(client.BaseAddress
                        + "api/Medications/" + model.Id, content).Result;

                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        // GET: Medications/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7269/");
                client.DefaultRequestHeaders.Add(AuthConstants.ApiKeyHeaderName, AuthConstants.ApiKeyHeaderValue);
                HttpResponseMessage response = client.DeleteAsync(client.BaseAddress
                    + "api/Medications/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    this.HttpContext.Session.Clear();

                }
                return RedirectToAction("Index");
            }
        }
        // POST: Medications/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var medication = await _context.Medication.FindAsync(id);
        //    if (medication != null)
        //    {
        //        _context.Medication.Remove(medication);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool MedicationExists(int id)
        //{
        //    return _context.Medication.Any(e => e.Id == id);
        //}
    }
}
