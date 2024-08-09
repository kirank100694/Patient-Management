using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PatientManagement.Data;
using PatientManagement.Models;
using PatientManagement.Repository;

namespace PatientManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientController(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        // GET: api/patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientCreateDate>>> GetPatients()
        {
            var patients = await _patientRepository.GetPatientsAsync();
            var patientDates = _mapper.Map<IEnumerable<PatientCreateDate>>(patients);
            return Ok(patientDates);
        }

        // GET: api/patients/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _patientRepository.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            var patientData = _mapper.Map<Patient>(patient);
            return Ok(patientData);
        }

        // POST: api/patients
        [HttpPost]
        public async Task<ActionResult<PatientCreateDate>> PostPatient(PatientCreateDate patientDates)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient = _mapper.Map<Patient>(patientDates);
            patient.CreatedDate = DateTime.UtcNow;
            patient.UpdatedDate = DateTime.UtcNow;

            await _patientRepository.AddPatientAsync(patient);
            var newPatientDate = _mapper.Map<PatientCreateDate>(patient);

            return CreatedAtAction(nameof(GetPatient), new { id = patient.Id }, newPatientDate);
        }

        // PUT: api/patients/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, PatientUpdateDate patientData)
        {
            if (id != patientData.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient = _mapper.Map<Patient>(patientData);
            patient.CreatedDate = DateTime.UtcNow;  // Set CreatedDate server-side
            patient.UpdatedDate = DateTime.UtcNow;  // Update UpdatedDate server-side

            await _patientRepository.UpdatePatientAsync(patient);
            return NoContent();
        }

        // PATCH: api/patients/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchPatient(int id, JsonPatchDocument<Patient> patchData)
        {
            if (patchData == null)
            {
                return BadRequest("Invalid patch document.");
            }

            var existingPatient = await _patientRepository.GetPatientByIdAsync(id);
            if (existingPatient == null)
            {
                return NotFound();
            }

            var patientData = _mapper.Map<Patient>(existingPatient);
            patchData.ApplyTo(patientData, ModelState);

            if (!TryValidateModel(patientData))
            {
                return ValidationProblem(ModelState);
            }

            var patient = _mapper.Map<Patient>(patientData);
            patient.CreatedDate = existingPatient.CreatedDate;  // Preserve original CreatedDate
            patient.UpdatedDate = DateTime.UtcNow;

            await _patientRepository.ApplyPatchAsync(id, patchData);
            return NoContent();
        }

        // DELETE: api/patients/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _patientRepository.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            await _patientRepository.DeletePatientAsync(id);
            return NoContent();
        }
    }
}
