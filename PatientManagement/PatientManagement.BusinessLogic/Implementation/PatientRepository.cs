using AutoMapper;
using LazyCache;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PatientManagement.Caching;
using PatientManagement.Data;
using PatientManagement.Helper;
using PatientManagement.Models;

namespace PatientManagement.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly PatientContext _patientContext;
        private readonly IMapper _mapper;
        private readonly ICacheProvider _cacheProvider;

        public PatientRepository(PatientContext patientContext, IMapper mapper, ICacheProvider cacheProvider)
        {
            _patientContext = patientContext;
            _mapper = mapper;
            _cacheProvider = cacheProvider;
        }

        public async Task<List<PatientModel>> GetPatients(PagingHelper pagingHelper)
        {
            var patients = _patientContext.Patients.AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(pagingHelper.name))
            {
                patients = patients.Where(m => m.FirstName.ToLower().Contains(pagingHelper.name) || m.Email.ToLower().Contains(pagingHelper.name));
            }

            // Sorting
            if (!string.IsNullOrEmpty(pagingHelper.sortBy))
            {
                if (pagingHelper.sortBy == "name")
                {
                    patients = pagingHelper.sortByDecending ? patients.OrderByDescending(m => m.FirstName) : patients.OrderBy(m => m.FirstName);
                }
                else if (pagingHelper.sortBy == "email")
                {
                    patients = pagingHelper.sortByDecending ? patients.OrderByDescending(m => m.Email) : patients.OrderBy(m => m.Email);
                }
            }

            // Pagination
            var totalItems = await patients.CountAsync();

            var pagedEmployees = await patients.Skip((pagingHelper.page - 1) * pagingHelper.pageSize).Take(pagingHelper.pageSize).ToListAsync();

            if (!_cacheProvider.TryGetValue(CacheKeys.Patient, out List<PatientModel> employeeModel))
            {
                var cacheEntryOption = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                    SlidingExpiration = TimeSpan.FromSeconds(30),
                    Size = 1024
                };
                _cacheProvider.Set(CacheKeys.Patient, patients, cacheEntryOption);
            }

            var employeeModels = _mapper.Map<List<PatientModel>>(pagedEmployees);

            return employeeModels;
        }

        public async Task<PatientModel> GetPatientsById(int patientId)
        {
            var patients = await _patientContext.Patients.FindAsync(patientId);

            if (!_cacheProvider.TryGetValue(CacheKeys.Patient, out List<PatientModel> patientModel))
            {
                var cacheEntryOption = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                    SlidingExpiration = TimeSpan.FromSeconds(30),
                    Size = 1024
                };
                _cacheProvider.Set(CacheKeys.Patient, patients, cacheEntryOption);
            }

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

        public async Task UpdatePatient(PatientModel existingPatient, PatientModel patientModel)
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
