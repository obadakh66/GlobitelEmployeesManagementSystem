using Globitel.Domain.Common;
using Globitel.Domain.DTO;
using Globitel.Domain.Models;
using Globitel.Repository.UnitOfWork;
using Globitel.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Globitel.Service.Services
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        AppConfig _appConfiguration = new AppConfig();

        public AuthService(
             UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<TokenResponseDTO> Login(LoginDTO model)
        {
            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (signInResult.Succeeded)
            {
                ApplicationUser user = _userManager.Users.FirstOrDefault(r => r.UserName == model.Username);

                if (user == null)
                {
                    throw new ValidationException("Username or password is wrong");
                }
                IList<string> roles = await _userManager.GetRolesAsync(user);
                IList<Claim> claims = await BuildClaims(user);
                TokenResponseDTO tokenResponse = BuildUserLoginObject(user, claims, roles);
                return tokenResponse;
            }
            else
            {
                throw new ValidationException("Something went wrong");
            }

        }

       



        private async Task<IList<Claim>> BuildClaims(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, string.IsNullOrEmpty(user.Id.ToString()) ? "" : user.Id.ToString()),
                new Claim(ClaimTypes.Name, string.IsNullOrEmpty(user.FullNameEN) ? "" : user.FullNameEN)
            };
            foreach (var item in roles)
            {
                var roleclaim = new Claim(ClaimTypes.Role, item);
                claims.Add(roleclaim);
            }


            return claims;
        }

       

        private TokenResponseDTO BuildUserLoginObject(ApplicationUser user, IList<Claim> claims, IList<string> roles)
        {
            TokenResponseDTO response = new TokenResponseDTO
            {
                AccessToken = WriteToken(claims)
            };

            return response;
        }
        private string WriteToken(IList<Claim> claims)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appConfiguration.JWTKey));

            JwtSecurityToken jwtToken = new JwtSecurityToken(
                    issuer: _appConfiguration.Issuer,
                    audience: _appConfiguration.Audience,
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddYears(100),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }
        //public DashboardResponseDTO GetDashboardData()
        //{
        //    IEnumerable<Business> businesses = _repositoryUnitOfWork.Business.Value.GetAll();
        //    IEnumerable<Person> persons = _repositoryUnitOfWork.Person.Value.GetAll();
        //    IEnumerable<ApplicationUser> applicationsUsers = _userManager.Users.ToList();

            
        //    DateTime thisWeekFirstDate = FirstDateOfWeek(DateTime.Now.Year, GetIso8601WeekOfYear(DateTime.Now));
        //    DateTime thisWeekLastDate = thisWeekFirstDate.AddDays(7);
        //    DashboardChartDTO dashboardChartDTO = new DashboardChartDTO();
        //    dashboardChartDTO.NumberOfApprovals = new List<int>();
        //    dashboardChartDTO.NumberOfUsers = new List<int>();
        //    int maximumNumberOfApprovals = 0;
        //    int maximumNumberOfUsers = 0;
        //    DateTime maximumNumberOfApprovalsDate = new DateTime();
        //    DateTime maximumNumberOfUsersDate = new DateTime();
        //    for (int day = 1; day <= 7; day++)
        //    {
        //        DateTime dateTime = thisWeekFirstDate.AddDays(day - 1).Date;
        //        int approvalCount = persons.Where(x => (!x.CreatedDate.HasValue) || x.CreatedDate.Value.Date == dateTime).Count() + businesses.Where(x => (!x.CreatedDate.HasValue) || x.CreatedDate.Value.Date == dateTime).Count();
        //        int usersCOunt = applicationsUsers.Where(x => (!x.CreationDate.HasValue) || x.CreationDate.Value.Date == dateTime).Count();
        //        if (approvalCount > maximumNumberOfApprovals)
        //        {
        //            maximumNumberOfApprovals = approvalCount;
        //            maximumNumberOfApprovalsDate = dateTime;
        //        }
        //        if (usersCOunt > maximumNumberOfUsers)
        //        {
        //            maximumNumberOfUsers = usersCOunt;
        //            maximumNumberOfUsersDate = dateTime;
        //        }

        //        dashboardChartDTO.NumberOfApprovals.Add(approvalCount);
        //        dashboardChartDTO.NumberOfUsers.Add(usersCOunt);
        //    }
        //    dashboardChartDTO.MaximumNumberOfApprovalsDate = maximumNumberOfApprovalsDate;
        //    dashboardChartDTO.MaximumNumberOfUsersDate = maximumNumberOfUsersDate;
        //    DashboardResponseDTO response = new DashboardResponseDTO
        //    {
        //        Dashboards = dashboards,
        //        Charts = dashboardChartDTO
        //    };
        //    return response;
        //}

    }
}
