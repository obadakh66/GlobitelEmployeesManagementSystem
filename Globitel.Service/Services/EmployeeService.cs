using Globitel.Domain.DTO;
using Globitel.Domain.Enums;
using Globitel.Domain.Models;
using Globitel.Repository.DbContext;
using Globitel.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Globitel.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private GlobitelDbContext _context;
        public EmployeeService(UserManager<ApplicationUser> userManager, GlobitelDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<ApplicationUser> Create(CreateEmployeeDTO model)
        {

            if (_userManager.Users.Any(x => x.PhoneNumber == model.MobileNumber))
            {
                throw new ValidationException("Mobile Number Already Exists");
            }
            if (_userManager.Users.Any(x => x.Email == model.Email))
            {
                throw new ValidationException("Email Already Exists");
            }
            ApplicationUser user = new ApplicationUser
            {
                Email = model.Email,
                FullNameEN = model.FullNameEN,
                FullNameAR = model.FullNameAR,
                IsActive = true,
                UserName = model.Email,
                Age = DateTime.Now.Year - model.DateOfBirth.Year,
                DateOfBirth = model.DateOfBirth,
                DateOfEmployement = model.DateOfEmployement,
                Gender = model.Gender,
                PhoneNumber = model.MobileNumber,
                PositionEN = model.PositionEN,
                PositionAR = model.PositionAR
            };


            IdentityResult result = await _userManager.CreateAsync(user, "123456");
            if (result.Succeeded)
            {
                CreateRoleIfNotExist("Employee", user.Id);
                return user;
            }
            throw new ValidationException("Create Employee Failed");
        }
        private void CreateRoleIfNotExist(string roleName, long userId)
        {
            Roles role = _context.Roles.FirstOrDefault(r => r.Name == roleName);
            if (role == null)
            {
                Roles roleObj = new Roles
                {
                    Id = 0,
                    Name = roleName
                };

                _context.Roles.Add(roleObj);
                _context.SaveChanges();
                role = roleObj;
            }
            UserRoles userRole = new UserRoles
            {
                RoleId = role.Id,
                UserId = userId
            };
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();
        }
        public async Task<CoreListResponse<EmployeeDTO>> GetAll(CoreListRequest request)
        {
            List<ApplicationUser> users = await _userManager
                .Users.ToListAsync();
            CoreListResponse<EmployeeDTO> response = new CoreListResponse<EmployeeDTO>
            {
                Entities = users.Where(x => (string.IsNullOrEmpty(request.SearchValue)) || (x.FullNameEN.ToUpper().Contains(request.SearchValue.ToUpper()) || x.FullNameAR.ToUpper().Contains(request.SearchValue.ToUpper())))
                .Select(user => new EmployeeDTO
                {
                    FullNameEN = user.FullNameEN,
                    FullNameAR = user.FullNameAR,
                    IsActive = user.IsActive,
                    Id = user.Id,
                    Age = user.Age,
                    DateOfBirth = user.DateOfBirth,
                    DateOfEmployement = user.DateOfEmployement,
                    Email = user.Email,
                    Gender = user.Gender,
                    MobileNumber = user.PhoneNumber,
                    PositionEN = user.PositionEN,
                    PositionAR = user.PositionAR
                }).Skip(request.PageSize * (request.PageIndex - 1)).Take(request.PageSize).ToList(),
                TotalCount = users.Count
            };


            return response;
        }
        public async Task<EmployeeDTO> GetById(int employeeId)
        {
            EmployeeDTO employee = await _userManager
                .Users.Where(x => x.Id == employeeId).Select(
                user => new EmployeeDTO
                {
                    FullNameEN = user.FullNameEN,
                    FullNameAR = user.FullNameAR,
                    IsActive = user.IsActive,
                    Id = user.Id,
                    Age = user.Age,
                    DateOfBirth = user.DateOfBirth,
                    DateOfEmployement = user.DateOfEmployement,
                    Email = user.Email,
                    Gender = user.Gender,
                    MobileNumber = user.PhoneNumber,
                    PositionEN = user.PositionEN,
                    PositionAR = user.PositionAR
                }).FirstOrDefaultAsync();



            return employee;
        }

        public Task<CoreListResponse<EmployeeDTO>> List()
        {
            throw new System.NotImplementedException();
        }
        public async Task<EmployeeDTO> UpdateEmployeeInfo(EmployeeDTO employeeInfo)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(employeeInfo.Id.ToString());

            user.Email = employeeInfo.Email;
            user.DateOfBirth = employeeInfo.DateOfBirth;
            user.FullNameEN = employeeInfo.FullNameEN;
            user.DateOfEmployement = employeeInfo.DateOfEmployement;
            user.Gender = employeeInfo.Gender;
            user.PhoneNumber = employeeInfo.MobileNumber;
            user.UserName = employeeInfo.Email;
            user.PositionAR = employeeInfo.PositionAR;
            user.Age = DateTime.Now.Year - employeeInfo.DateOfBirth.Year;


            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return employeeInfo; ;
            }
            throw new Exception("Error in Updating Employee Info");
        }
        public async Task<MemoryStream> ExportActiveEmployeesToExcel(Language language)
        {
            List<ApplicationUser> activeEmployees = await _userManager.GetUsersInRoleAsync("Employee").Where(x => (bool)x.IsActive).ToListAsync();

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add(language == Language.English ? "Active Employees" : "الموظفين النشطين");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(Color.Blue);
                const int startRow = 5;
                var row = startRow;

                //Create Headers and format them
                worksheet.Cells["A1"].Value = language == Language.English ? "Active Employees" : "الموظفين النشطين";
                using (var r = worksheet.Cells["A1:C1"])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(Color.White);
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
                }

                worksheet.Cells["A4"].Value = language == Language.English ? "Name" : "الاسم";
                worksheet.Cells["B4"].Value = language == Language.English ? "Email" : "البريد الالكتروني";
                worksheet.Cells["C4"].Value = language == Language.English ? "Mobile Number" : "رقم الهاتف";
                worksheet.Cells["D4"].Value = language == Language.English ? "Age" : "العمر";
                worksheet.Cells["A4:D4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A4:D4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                worksheet.Cells["A4:D4"].Style.Font.Bold = true;

                row = 5;
                foreach (var emp in activeEmployees)
                {
                    worksheet.Cells[row, 1].Value = language == Language.English ? emp.FullNameEN : emp.FullNameAR;
                    worksheet.Cells[row, 2].Value = emp.Email;
                    worksheet.Cells[row, 3].Value = emp.PhoneNumber;
                    worksheet.Cells[row, 4].Value = emp.Age;

                    row++;
                }

                // set some core property values
                xlPackage.Workbook.Properties.Title = language == Language.English ? "Globitel Active Employees List" : "قائمة المموظفين النشطين لشركة Globitel";
                xlPackage.Workbook.Properties.Author = language == Language.English ? "Obada Alkhdor" : "عبادة الخضور";
                xlPackage.Workbook.Properties.Subject = "Globitel";
                // save the new spreadsheet
                xlPackage.Save();
                // Response.Clear();
            }
            stream.Position = 0;
            return stream;

        }
        public async Task<bool> UpdateStatus(long employeeId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(employeeId.ToString());
            user.IsActive = !user.IsActive;
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
        public async Task<DashboardDTO> GetDashboardData()
        {
            IList<ApplicationUser> employees = await _userManager.GetUsersInRoleAsync("Employee");
            DashboardDTO dashboard = new DashboardDTO
            {
                AgeChart = new AgeChartDTO
                {
                    SmallClassCount = employees.Where(x => x.Age >= 20 && x.Age <= 40).Count(),
                    MedClassCount = employees.Where(x => x.Age >= 40 && x.Age <= 60).Count(),
                    OldClassCount = employees.Where(x => x.Age >= 60 && x.Age <= 70).Count()
                },
                GenderChart = new GenderChartDTO
                {
                    FemaleCount = employees.Where(x => x.Gender == GenderEnum.Female).Count(),
                    MaleCount = employees.Where(x => x.Gender == GenderEnum.Male).Count()
                }
            };

            return dashboard;
        }



    }
}
