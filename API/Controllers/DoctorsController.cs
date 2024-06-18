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
    public class DoctorsController : ControllerBase
    {
        private readonly APIContext _context;

        public DoctorsController(APIContext context)
        {
            _context = context;
        }

        // GET: api/Doctors
        [HttpGet]
        public async Task<ActionResult<List<DoctorDTO>>> GetDoctor()
        {
            return ConversionServices.DoctorToDTO(await _context.Doctor.ToListAsync());
            //return await _context.Doctor
            //return await _context.Doctor.ToListAsync();
        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDTO>> GetDoctor(int id)
        {
            var doctorDTO = ConversionServices.DoctorToDTO(await _context.Doctor.FindAsync(id));

            if (doctorDTO == null)
            {
                return NotFound();
            }

            return doctorDTO;
        }

        // PUT: api/Doctors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(int id, DoctorDTO doctorDTO)
        {
            Doctor doctor = ConversionServices.DoctorToEntity(doctorDTO);
            if (id != doctor.Id)
            {
                return BadRequest();
            }

            _context.Entry(doctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
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

        // POST: api/Doctors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Doctor>> PostDoctor(DoctorDTO doctorDTO)
        {
            _context.Doctor.Add(ConversionServices.DoctorToEntity(doctorDTO));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoctor", new { id = doctorDTO.Id }, doctorDTO);
        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctor = await _context.Doctor.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctor.Remove(doctor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctor.Any(e => e.Id == id);
        }
    }
}
