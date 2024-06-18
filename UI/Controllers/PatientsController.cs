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
using UI.ViewModels.Patients;
using X.PagedList;

namespace UI.Controllers
{
    public class PatientsController : Controller
    {
        private readonly UIContext _context;

        public PatientsController(UIContext context)
        {
            _context = context;
        }

        // GET: Patients
        //public async Task<IActionResult> Index()
        //{
        //    var uIContext = _context.Patient.Include(p => p.Doctor).Include(p => p.Medication);
        //    return View(await uIContext.ToListAsync());
        //}
        public async Task<IActionResult> Index(PIndexVM model, int? page, string? name)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            JObject data = new JObject();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7269/");
                client.DefaultRequestHeaders.Add(AuthConstants.ApiKeyHeaderName, AuthConstants.ApiKeyHeaderValue);
                try
                {
                    response = client.GetAsync(client.BaseAddress
                       + "api/Patients/").Result;
                }
                catch (Exception)
                {

                    throw;
                }
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    model.Items = JsonConvert.DeserializeObject<List<PatientVM>>(result);
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            //    var doctors = from d in _context.Doctor
            //               select d;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    doctors = doctors.Where(s => s.LName.Contains(searchString)
            //                           || s.FName.Contains(searchString));
            //}
            //return View(doctors.ToList());
            if (name != null)
            {
                model.Items = model.Items.Where(x => x.FName.ToLower() == name.ToLower()).ToList();
            }
            int pageNumber = page ?? 1;
            int pageSize = 10;
            var pagedDoctors = model.Items.ToPagedList(pageNumber, pageSize);
            return View(pagedDoctors);

            //var patients = from p in _context.Patient
            //              select p;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    patients = patients.Where(p => p.LName.Contains(searchString)
            //                           || p.FName.Contains(searchString));
            //}
            //var uIContext = patients.Include(p => p.Doctor).Include(p => p.Medication);
            //return View(await uIContext.ToListAsync());
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            PatientVM model = new PatientVM();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7269/");
                client.DefaultRequestHeaders.Add(AuthConstants.ApiKeyHeaderName, AuthConstants.ApiKeyHeaderValue);
                response = client.GetAsync(client.BaseAddress
                   + "api/Patients/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<PatientVM>(result);
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

        // GET: Patients/Create
        public IActionResult Create(int id)
        {
            PCreateVM model = new PCreateVM();
            model.Id = id;
            return View(model);
            //ViewData["DoctorId"] = new SelectList(_context.Doctor, "Id", "FName");
            //ViewData["MedicationId"] = new SelectList(_context.Medication, "Id", "Name");
            //return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PCreateVM model)
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
                        + "api/Patients", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        data = response.Content.ReadAsStringAsync().Result;
                        JObject result = JObject.Parse(data);
                    }


                    return RedirectToAction("Index");

                }
                //_context.Add(doctor);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            return View(model);
            //if (patient.FName.Length < 21 && patient.LName.Length < 21)
            //{
            //    _context.Add(patient);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["DoctorId"] = new SelectList(_context.Doctor, "Id", "FName", patient.DoctorId);
            //ViewData["MedicationId"] = new SelectList(_context.Medication, "Id", "Name", patient.MedicationId);
            //return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            PCreateVM model = new PCreateVM();
            HttpResponseMessage response = new HttpResponseMessage();
            JObject data = new JObject();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7269/");
                client.DefaultRequestHeaders.Add(AuthConstants.ApiKeyHeaderName, AuthConstants.ApiKeyHeaderValue);
                response = client.GetAsync(client.BaseAddress
                    + "api/Patients/" + id).Result;
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
            //if (data.GetValue<int>("id") != null)
            model.Id = data.GetValue<int>("id");
            model.Gender = data.GetValue<char>("gender");
            model.FName = data.GetValue<string>("fName");
            model.LName = data.GetValue<string>("lName");
            model.BirthDate = data.GetValue<DateTime>("birthDate");
            model.DoctorId = data.GetValue<int>("doctorId");
            model.MedicationId = data.GetValue<int>("medicationId");
            //if (data.GetValue<bool>("isMale"))
            //{
            //    model.Gender = Gender.Male;
            //}
            //else
            //{
            //    model.Gender = Gender.Female;
            //}

            return View(model);
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var patient = await _context.Patient.FindAsync(id);
            //if (patient == null)
            //{
            //    return NotFound();
            //}
            //ViewData["DoctorId"] = new SelectList(_context.Doctor, "Id", "FName", patient.DoctorId);
            //ViewData["MedicationId"] = new SelectList(_context.Medication, "Id", "Name", patient.MedicationId);
            //return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PCreateVM model)
        {
            if (ModelState.IsValid)
            {
                //model.IsMale = model.Gender == 0;

                HttpResponseMessage response = new HttpResponseMessage();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7269/");
                    client.DefaultRequestHeaders.Add(AuthConstants.ApiKeyHeaderName, AuthConstants.ApiKeyHeaderValue);
                    string data = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent(data, Encoding.UTF8,
                        "application/json");
                    response = client.PutAsync(client.BaseAddress
                        + "api/Patients/" + model.Id, content).Result;

                    return RedirectToAction("Index");
                }
            }
            return View(model);
            //if (id != patient.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(patient);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!PatientExists(patient.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["DoctorId"] = new SelectList(_context.Doctor, "Id", "FName", patient.DoctorId);
            //ViewData["MedicationId"] = new SelectList(_context.Medication, "Id", "Name", patient.MedicationId);
            //return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7269/");
                client.DefaultRequestHeaders.Add(AuthConstants.ApiKeyHeaderName, AuthConstants.ApiKeyHeaderValue);
                HttpResponseMessage response = client.DeleteAsync(client.BaseAddress
                    + "api/Patients/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    this.HttpContext.Session.Clear();

                }
                return RedirectToAction("Index");

            }
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var patient = await _context.Patient
            //    .Include(p => p.Doctor)
            //    .Include(p => p.Medication)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (patient == null)
            //{
            //    return NotFound();
            //}

            //return View(patient);
        }

        // POST: Patients/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var patient = await _context.Patient.FindAsync(id);
        //    if (patient != null)
        //    {
        //        _context.Patient.Remove(patient);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool PatientExists(int id)
        //{
        //    return _context.Patient.Any(e => e.Id == id);
        //}
    }
}
