using Microsoft.AspNetCore.JsonPatch;
using PatientManagement.Migrations;
using PatientManagement.Models;

namespace PatientManagement.Repository
{
    public interface IPatientRepository
    {
        Task<List<PatientModel>> GetPatients();

        Task<PatientModel> GetPatientsById(int patientId);

        Task<int> AddPatients(PatientModel patientModel);

        Task UpdatePatients(PatientModel existingPatient, PatientModel patientModel);

        Task UpdatePatients(PatientModel existingPatient, JsonPatchDocument patientModel);

        Task DeletePatientsById(int patientId);

        Task<bool> IsPatientsExists(int id);

        Task<bool> IsPatientsExists(string email);
    }
}