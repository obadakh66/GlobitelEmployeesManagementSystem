using Globitel.Domain.DTO;
using Globitel.Domain.Enums;
using Globitel.Domain.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Globitel.Service.Interfaces
{
    public interface IEmployeeService 
    {
        Task<CoreListResponse<EmployeeDTO>> List();
        Task<CoreListResponse<EmployeeDTO>> GetAll(CoreListRequest request);
        Task<bool> UpdateStatus(long employeeId);
        Task<ApplicationUser> Create(CreateEmployeeDTO model);
        Task<EmployeeDTO> UpdateEmployeeInfo(EmployeeDTO employeeInfo);
        Task<MemoryStream> ExportActiveEmployeesToExcel(Language language);
        Task<EmployeeDTO> GetById(int employeeId);
        Task<DashboardDTO> GetDashboardData();
    }
}


