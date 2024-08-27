using AutoMapper;
using PatientManagement.Models;

namespace PatientManagement.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Patient, PatientModel>().ReverseMap();
        }
    }
}
