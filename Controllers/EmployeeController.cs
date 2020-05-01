using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackendApi.Models;
using BackendApi.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [Route("api/employees")]
    [ApiController]    
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            var empFromRepo = _employeeRepository.GetEmployees();
            return Ok(_mapper.Map<IEnumerable<Employee>>(empFromRepo));
        }

        [HttpGet("{empId:int}")]
        public ActionResult<Employee> GetEmployee(int empId)
        {
            var empFromRepo = _employeeRepository.GetEmployee(empId);

            if (!_employeeRepository.EmployeeExists(empId))
            {
                return NotFound();
            }                

            return Ok(_mapper.Map<Employee>(empFromRepo));
        }

        public void AddEmployee(Employee emp)
        {
            if(emp != null)
            {
                _employeeRepository.AddEmployee(emp);
            }
        }

    }
}