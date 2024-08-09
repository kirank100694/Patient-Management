using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using PatientManagement.Data;
using PatientManagement.Models;

namespace PatientManagement.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly PatientContext _patientContext;

        public PatientRepository(PatientContext patientContext)
        {
            _patientContext = patientContext;
        }

        public async Task<List<Patient>> GetPatientsAsync()
        {
            return await _patientContext.Patients.ToListAsync();
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            return await _patientContext.Patients.FindAsync();
        }

        public async Task AddPatientAsync(Patient patient)
        {
            _patientContext.Patients.Add(patient);
            await _patientContext.SaveChangesAsync();
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            _patientContext.Entry(patient).State = EntityState.Modified;
            await _patientContext.SaveChangesAsync();
        }

        public async Task ApplyPatchAsync(int id, JsonPatchDocument<Patient> patchDoc)
        {
            var patient = await _patientContext.Patients.FindAsync(id);
            if (patient == null)
            {
                throw new KeyNotFoundException("Patient not found.");
            }

            patchDoc.ApplyTo(patient);
            patient.UpdatedDate = DateTime.UtcNow;
            _patientContext.Entry(patient).State = EntityState.Modified;
            await _patientContext.SaveChangesAsync();
        }

        public async Task DeletePatientAsync(int id)
        {
            var patient = await _patientContext.Patients.FindAsync(id);
            if (patient != null)
            {
                _patientContext.Patients.Remove(patient);
                await _patientContext.SaveChangesAsync();
            }
        }

        
    }
}
