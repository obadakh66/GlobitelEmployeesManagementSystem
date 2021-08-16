using Globitel.Domain.DTO;
using Globitel.Domain.Enums;
using Globitel.Domain.Models;
using Globitel.Service.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace GlobitelEmployeesManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        public EmployeeController(IServiceUnitOfWork serviceUnitOfWork)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetList([FromBody]CoreListRequest request)
        {
            try
            {
                return Ok(await _serviceUnitOfWork.Employee.Value.GetAll(request));
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeDTO createDTO)
        {
            try
            {
                ApplicationUser applicationUser = await _serviceUnitOfWork.Employee.Value.Create(createDTO);
                return Ok(applicationUser);
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult ChangeStatus(int employeeId)
        {
            try
            {
                return Ok(_serviceUnitOfWork.Employee.Value.UpdateStatus(employeeId));
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetById(int employeeId)
        {
            try
            {
                return Ok(await _serviceUnitOfWork.Employee.Value.GetById(employeeId));
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit([FromBody] EmployeeDTO employee)
        {
            try
            {
                return Ok(_serviceUnitOfWork.Employee.Value.UpdateEmployeeInfo(employee));
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ExportActiveEmployeesToExcel(Language language)
        {
            try
            {
                MemoryStream stream = await _serviceUnitOfWork.Employee.Value.ExportActiveEmployeesToExcel(language);
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", language == Language.English ? "Globitel Active Employees List.xlsx": "قائمة المموظفين النشطين لشركة Globitel.xlsx");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        } 
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetDashboardData()
        {
            try
            {
                return Ok(await _serviceUnitOfWork.Employee.Value.GetDashboardData());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }
    }
}
