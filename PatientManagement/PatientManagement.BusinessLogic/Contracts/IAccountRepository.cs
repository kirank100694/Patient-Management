using Microsoft.AspNetCore.Identity;
using PatientManagement.Models;

namespace PatientManagement.Repository
{
    public interface IAccountRepository
    {
        Task<string> Login(SignInModel signInModel);
        Task<IdentityResult> Register(SignUpModel signUpModel);
    }
}