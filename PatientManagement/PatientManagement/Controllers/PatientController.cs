using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PatientManagement.Data;
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

        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            var patients = await _patientRepository.GetPatients();

            if (patients != null && patients.Count == 0)
            {
                return BadRequest("No patients found.");
            }

            return Ok(patients);
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
        public async Task<ActionResult> UpdatePatients([FromBody] PatientModel patientModel, [FromRoute] int id)
        {
            var existingPatient = await _patientRepository.GetPatientsById(id);
            if (existingPatient == null)
            {
                return BadRequest($"Employee Id {id} is not found.");
            }

            await _patientRepository.UpdatePatients(existingPatient, patientModel);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdatePatients([FromBody] JsonPatchDocument patientModel, [FromRoute] int id)
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
