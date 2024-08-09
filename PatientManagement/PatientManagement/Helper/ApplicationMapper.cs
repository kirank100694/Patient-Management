using AutoMapper;
using PatientManagement.Data;
using PatientManagement.Models;

namespace PatientManagement.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Patient, PatientCreateDate>().ReverseMap();
            CreateMap<Patient, PatientUpdateDate>().ReverseMap();
        }
    }
}
