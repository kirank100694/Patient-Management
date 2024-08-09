using Microsoft.AspNetCore.JsonPatch;
using PatientManagement.Models;

namespace PatientManagement.Repository
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetPatientsAsync();
        Task<Patient> GetPatientByIdAsync(int id);
        Task AddPatientAsync(Patient patient);
        Task UpdatePatientAsync(Patient patient);
        Task ApplyPatchAsync(int id, JsonPatchDocument<Patient> patchDoc);
        Task DeletePatientAsync(int id);
        
       
    }
}