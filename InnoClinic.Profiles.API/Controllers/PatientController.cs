using InnoClinic.Profiles.API.Contracts;
using InnoClinic.Profiles.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace InnoClinic.Profiles.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreatePatientAsync(CreatePatientRequest createPatientRequest)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            await _patientService.CreatePatientAsync(createPatientRequest.FirstName, createPatientRequest.LastName,
                createPatientRequest.MiddleName, createPatientRequest.PhoneNumber, createPatientRequest.IsLinkedToAccount, 
                createPatientRequest.DateOfBirth, token);

            return Ok();
        }

        [Authorize]
        [HttpPost("force-create-patient")]
        public async Task<ActionResult> ForceCreatePatientAsync(CreatePatientRequest createPatientRequest)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            await _patientService.ForceCreatePatientAsync(createPatientRequest.FirstName, createPatientRequest.LastName,
                createPatientRequest.MiddleName, createPatientRequest.PhoneNumber, createPatientRequest.IsLinkedToAccount,
                createPatientRequest.DateOfBirth, token);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPatientsAsync()
        {
            return Ok(await _patientService.GetAllPatientsAsync());
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdatePatientAsync(Guid id, UpdatePatientRequest updatePatientRequest)
        {
            await _patientService.UpdatePatientAsync(id, updatePatientRequest.FirstName, updatePatientRequest.LastName,
                updatePatientRequest.MiddleName, updatePatientRequest.IsLinkedToAccount, updatePatientRequest.DateOfBirth,
                updatePatientRequest.AccountId);

            return Ok();
        }

        [Authorize]
        [HttpPut("account-connection-with-the-patient/{id:guid}")]
        public async Task<ActionResult> AccountConnectionWithThePatientAsync([FromBody] AccountConnectionWithThePatientRequest accountConnectionWithThePatientRequest)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            await _patientService.AccountConnectionWithThePatient(token, accountConnectionWithThePatientRequest.PatientId);

            return Ok();
        }


        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeletePatientAsync(Guid id)
        {
            await _patientService.DeletePatientAsync(id);

            return Ok();
        }
    }
}
