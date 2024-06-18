using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Data.Entities;
using API.DTOs;
using API.Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly APIContext _context;

        public PatientsController(APIContext context)
        {
            _context = context;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<List<PatientDTO>>> GetPatient()
        {
            return ConversionServices.PatientToDTO(await _context.Patient.ToListAsync());
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDTO>> GetPatient(int id)
        {
            var patientDTO = ConversionServices.PatientToDTO(await _context.Patient.FindAsync(id));

            if (patientDTO == null)
            {
                return NotFound();
            }

            return patientDTO;
        }

        // PUT: api/Patients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, PatientDTO patientDTO)
        {
            Patient patient = ConversionServices.PatientToEntity(patientDTO);
            if (id != patient.Id)
            {
                return BadRequest();
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Patients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(PatientDTO patientDTO)
        {
            _context.Patient.Add(ConversionServices.PatientToEntity(patientDTO));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatient", new { id = patientDTO.Id }, patientDTO);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patientDTO = ConversionServices.PatientToDTO(await _context.Patient.FindAsync(id));
            if (patientDTO == null)
            {
                return NotFound();
            }

            _context.Patient.Remove(ConversionServices.PatientToEntity(patientDTO));
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientExists(int id)
        {
            return _context.Patient.Any(e => e.Id == id);
        }
    }
}
