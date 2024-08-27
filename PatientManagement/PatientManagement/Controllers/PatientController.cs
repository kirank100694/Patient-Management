using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PatientManagement.Helper;
using PatientManagement.Models;
using PatientManagement.Repository;

namespace PatientManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPatients([FromBody] PagingHelper pagingHelper)
        {
            var patirnts = await _patientRepository.GetPatients(pagingHelper);

            if (patirnts != null && patirnts.Count == 0)
            {
                return BadRequest("No Patient found.");
            }

            return Ok(patirnts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPatientsById([FromRoute] int id)
        {
            var patients = await _patientRepository.GetPatientsById(id);

            if (patients == null)
            {
                return BadRequest($"patient with ID {id} not found.");
            }

            return Ok(patients);
        }

        [HttpPost]
        public async Task<ActionResult> AddPatients([FromBody] PatientModel patientModel)
        {
            if (await _patientRepository.IsPatientsExists(patientModel.ContactNumber))
            {
                return BadRequest("patient is already exists.");
            }
            var id = await _patientRepository.AddPatients(patientModel);

            return CreatedAtAction(nameof(GetPatientsById), new { id = id, Controller = "patient" }, id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePatient([FromBody] PatientModel patientModel, [FromRoute] int id)
        {
            var existingPatient = await _patientRepository.GetPatientsById(id);
            if (existingPatient == null)
            {
                return BadRequest($"Employee Id {id} is not found.");
            }

            await _patientRepository.UpdatePatient(existingPatient, patientModel);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdatePatients([FromBody] JsonPatchDocument patientModel, 
            [FromRoute] int id)
        {
            var existingPatient = await _patientRepository.GetPatientsById(id);
            if (existingPatient == null)
            {
                return BadRequest($"Patient Id {id} is not found.");
            }

            await _patientRepository.UpdatePatients(existingPatient, patientModel);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePatientsById(int id)
        {

            if (!await _patientRepository.IsPatientsExists(id))
            {
                return BadRequest($"Patient Id {id} is not found.");
            }

            await _patientRepository.DeletePatientsById(id);
            return Ok();
        }
    }
}
