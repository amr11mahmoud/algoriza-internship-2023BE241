using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Consts;
using Vezeeta.Core.Service.Users;
using Vezeeta.Service.Dtos.Response.Patients;
using Vezeeta.Web.Helpers;

namespace Vezeeta.Web.Controllers.Admin
{
    [Route("api/admin/patients/[action]")]
    public class AdminPatientsController : ApplicationController
    {
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;

        public AdminPatientsController(IPatientService patientService, IMapper mapper)
        {
            _patientService = patientService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPatientDto>>> GetAll(int page = 1, int pageSize = 10, string search = "")
        {
            var getPatientsResult = await _patientService.GetAllPatientsAsync(page, pageSize, search);

            if (getPatientsResult.IsFailure)
            {
                return BadRequest(getPatientsResult.Error);
            }

            var response = _mapper.Map<IEnumerable<GetPatientDto>>(getPatientsResult.Value);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<GetPatientDto>> GetById(int id)
        {
            var getPatientResult = await _patientService.GetPatientWithBookingsAsync(id);

            if (getPatientResult.IsFailure)
            {
                return BadRequest(getPatientResult.Error);
            }

            var response = getPatientResult.Value;

            return Ok(response);
        }
    }
}
