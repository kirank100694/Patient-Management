using Microsoft.AspNetCore.JsonPatch;
using PatientManagement.Helper;
using PatientManagement.Models;

namespace PatientManagement.Repository
{
    public interface IPatientRepository
    {
        Task<List<PatientModel>> GetPatients(PagingHelper pagingHelper);

        Task<PatientModel> GetPatientsById(int patientId);

        Task<int> AddPatients(PatientModel patientModel);

        Task UpdatePatient(PatientModel existingPatient, PatientModel patientModel);

        Task UpdatePatients(PatientModel existingPatient, JsonPatchDocument patientModel);

        Task DeletePatientsById(int patientId);

        Task<bool> IsPatientsExists(int id);

        Task<bool> IsPatientsExists(string email);
    }
}