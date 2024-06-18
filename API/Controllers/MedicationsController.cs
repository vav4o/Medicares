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
    public class MedicationsController : ControllerBase
    {
        private readonly APIContext _context;

        public MedicationsController(APIContext context)
        {
            _context = context;
        }

        // GET: api/Medications
        [HttpGet]
        public async Task<ActionResult<List<MedicationDTO>>> GetMedication()
        {
            return ConversionServices.MedicationToDTO(await _context.Medication.ToListAsync());
           //return await _context.Medication.ToListAsync();
        }

        // GET: api/Medications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicationDTO>> GetMedication(int id)
        {
            var medicationDTO = ConversionServices.MedicationToDTO(await _context.Medication.FindAsync(id));

            if (medicationDTO == null)
            {
                return NotFound();
            }

            return medicationDTO;
        }

        // PUT: api/Medications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedication(int id, MedicationDTO medicationDTO)
        {
            Medication medication = ConversionServices.MedicationToEntity(medicationDTO);
            if (id != medication.Id)
            {
                return BadRequest();
            }

            _context.Entry(medication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicationExists(id))
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

        // POST: api/Medications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Medication>> PostMedication(MedicationDTO medicationDTO)
        {
            _context.Medication.Add(ConversionServices.MedicationToEntity(medicationDTO));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedication", new { id = medicationDTO.Id }, medicationDTO);
        }

        // DELETE: api/Medications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedication(int id)
        {
            var medicationDTO = ConversionServices.MedicationToDTO(await _context.Medication.FindAsync(id));
            if (medicationDTO == null)
            {
                return NotFound();
            }

            _context.Medication.Remove(ConversionServices.MedicationToEntity(medicationDTO));
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicationExists(int id)
        {
            return _context.Medication.Any(e => e.Id == id);
        }
    }
}
