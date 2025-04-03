﻿using InnoClinic.Profiles.Application.Services;
using InnoClinic.Profiles.Core.Models.PatientModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("create")]
        public async Task<ActionResult> CreateAdminPatientAsync([FromBody] CreatePatientRequest createPatientRequest)
        {
            await _patientService.CreatePatientAsync(createPatientRequest.FirstName, createPatientRequest.LastName,
                createPatientRequest.MiddleName, createPatientRequest.DateOfBirth);

            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreatePatientAsync([FromBody] CreatePatientRequest createPatientRequest)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            await _patientService.CreatePatientAsync(createPatientRequest.FirstName, createPatientRequest.LastName,
                createPatientRequest.MiddleName, createPatientRequest.PhoneNumber, createPatientRequest.IsLinkedToAccount, 
                createPatientRequest.DateOfBirth, createPatientRequest.PhotoId, token);

            return Ok();
        }

        [Authorize]
        [HttpPost("force-create-patient")]
        public async Task<ActionResult> ForceCreatePatientAsync([FromBody] CreatePatientRequest createPatientRequest)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            await _patientService.ForceCreatePatientAsync(createPatientRequest.FirstName, createPatientRequest.LastName,
                createPatientRequest.MiddleName, createPatientRequest.PhoneNumber, createPatientRequest.IsLinkedToAccount,
                createPatientRequest.DateOfBirth, createPatientRequest.PhotoId, token);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPatientsAsync()
        {
            return Ok(await _patientService.GetAllPatientsAsync());
        }

        //[Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetPatientByIdAsync(Guid id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        [Authorize]
        [HttpGet("patient-by-account-id-from-token")]
        public async Task<ActionResult> GetPatientByAccountIdFromTokenAsync()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return Ok(await _patientService.GetPatientByAccountIdFromTokenAsync(token));
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdatePatientAsync(Guid id, [FromBody] UpdatePatientRequest updatePatientRequest)
        {
            await _patientService.UpdatePatientAsync(id, updatePatientRequest.FirstName, updatePatientRequest.LastName,
                updatePatientRequest.MiddleName, updatePatientRequest.IsLinkedToAccount, updatePatientRequest.DateOfBirth, updatePatientRequest.PhotoId);

            return Ok();
        }

        [Authorize]
        [HttpPut("account-connection-with-the-patient/{id:guid}")]
        public async Task<ActionResult> AccountConnectionWithThePatientAsync(Guid id)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            await _patientService.AccountConnectionWithThePatient(token, id);

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
