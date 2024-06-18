using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using API.Authentication;
using Azure;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.ProjectModel;
using UI.Data;
using UI.Models;
using UI.ViewModels.Doctors;
using X.PagedList;

namespace UI.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly UIContext _context;

        public DoctorsController(UIContext context)
        {
            _context = context;
        }

        // GET: Doctors
        public async Task<IActionResult> Index(IndexVM model, int? page, string? name)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7269/");
                client.DefaultRequestHeaders.Add(AuthConstants.ApiKeyHeaderName, AuthConstants.ApiKeyHeaderValue);
                try
                {
                    response = client.GetAsync(client.BaseAddress
                       + "api/Doctors/").Result;
                }
                catch (Exception)
                {

                    throw;
                }
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    model.Items = JsonConvert.DeserializeObject<List<DoctorVM>>(result);
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
            int pageNumber = page?? 1;
            int pageSize = 10;
            var pagedDoctors = model.Items.ToPagedList(pageNumber, pageSize);
            return View(pagedDoctors);
        }

        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            DoctorVM model = new DoctorVM();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7269/");
                client.DefaultRequestHeaders.Add(AuthConstants.ApiKeyHeaderName, AuthConstants.ApiKeyHeaderValue);
                response = client.GetAsync(client.BaseAddress
                   + "api/Doctors/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<DoctorVM>(result);
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
            //HttpResponseMessage response = new HttpResponseMessage();
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var doctor = await _context.Doctor
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (doctor == null)
            //{
            //    return NotFound();
            //}

            //return View(doctor);
        }

        // GET: Doctors/Create
        public IActionResult Create(int id)
        {
            CreateVM model = new CreateVM();
            model.Id = id;
            return View(model);
        }

        //POST: Doctors/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to.
        //For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVM model)
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
                        + "api/Doctors", content).Result;
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
        }

        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            CreateVM model = new CreateVM();
            HttpResponseMessage response = new HttpResponseMessage();
            JObject data = new JObject();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7269/");
                client.DefaultRequestHeaders.Add(AuthConstants.ApiKeyHeaderName, AuthConstants.ApiKeyHeaderValue);
                response = client.GetAsync(client.BaseAddress
                    + "api/Doctors/" + id).Result;
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
            model.HireDate = (data.Value<DateTime>("hireDate") == null) ? null : data.GetValue<DateTime>("hireDate");
            model.Email = (data.Value<string>("email") == null) ? null : data.GetValue<string>("email");
            model.Salary = (data.Value<decimal>("salary") == null) ? 0 : data.GetValue<decimal>("salary");
            model.PhoneNumber = (data.Value<string>("phoneNumber") == null) ? null : data.GetValue<string>("phoneNumber");
            model.Id = data.GetValue<int>("id");
            model.Username = data.GetValue<string>("username");
            model.FName = data.GetValue<string>("fName");
            model.LName = data.GetValue<string>("lName");
            model.Password = data.GetValue<string>("password");
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

            //var doctor = await _context.Doctor.FindAsync(id);
            //if (doctor == null)
            //{
            //    return NotFound();
            //}
            //return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateVM model)
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
                        + "api/Doctors/" + model.Id, content).Result;

                    return RedirectToAction("Index");
                }
            }
            return View(model);
            
            //if (id != doctor.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(doctor);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!DoctorExists(doctor.Id))
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
            //return View(doctor);
        }

        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7269/");
                client.DefaultRequestHeaders.Add(AuthConstants.ApiKeyHeaderName, AuthConstants.ApiKeyHeaderValue);
                HttpResponseMessage response = client.DeleteAsync(client.BaseAddress
                    + "api/Doctors/" + id).Result;
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

            //var doctor = await _context.Doctor
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (doctor == null)
            //{
            //    return NotFound();
            //}

            //return View(doctor);
        }

        // POST: Doctors/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var doctor = await _context.Doctor.FindAsync(id);
        //    if (doctor != null)
        //    {
        //        _context.Doctor.Remove(doctor);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool DoctorExists(int id)
        //{
        //    return _context.Doctor.Any(e => e.Id == id);
        //}
    }
}
