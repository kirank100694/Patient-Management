using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using PatientManagement.Data;
using PatientManagement.Migrations;
using PatientManagement.Models;

namespace PatientManagement.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly PatientContext _patientContext;
        private readonly IMapper _mapper;

        public PatientRepository(PatientContext patientContext, IMapper mapper)
        {
            _patientContext = patientContext;
            _mapper = mapper;
        }

        public async Task<List<PatientModel>> GetPatients()
        {
            var patients = await _patientContext.Patients.ToListAsync();
            return _mapper.Map<List<PatientModel>>(patients);
        }

        public async Task<PatientModel> GetPatientsById(int patientId)
        {
            var patients = await _patientContext.Patients.FindAsync(patientId);
            return _mapper.Map<PatientModel>(patients);
        }

        public async Task<int> AddPatients(PatientModel patientModel)
        {
            var patient = _mapper.Map<PatientModel>(patientModel);
            patient.CreatedDate = DateTime.Now;

            _patientContext.Patients.Add(patient);
            await _patientContext.SaveChangesAsync();

            return patient.Id;
        }

        public async Task UpdatePatients(PatientModel existingPatient, PatientModel patientModel)
        {
            _mapper.Map(patientModel, existingPatient);

            existingPatient.UpdatedDate = DateTime.Now;

            await _patientContext.SaveChangesAsync();
        }

        public async Task UpdatePatients(PatientModel existingPatient, JsonPatchDocument patientModel)
        {
            patientModel.ApplyTo(existingPatient);

            existingPatient.UpdatedDate = DateTime.Now;

            await _patientContext.SaveChangesAsync();
        }

        public async Task DeletePatientsById(int patientId)
        {
            var patient = new PatientModel() { Id = patientId };

            _patientContext.Patients.Remove(patient);

            await _patientContext.SaveChangesAsync();
        }

        public async Task<bool> IsPatientsExists(int id)
        {
            return await _patientContext.Patients.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> IsPatientsExists(string contactNumber)
        {
            return await _patientContext.Patients.AnyAsync(c => c.ContactNumber == contactNumber);
        }
    }
}
