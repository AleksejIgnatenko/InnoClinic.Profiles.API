using InnoClinic.Profiles.Application.Services;
using InnoClinic.Profiles.Core.Models.DoctorModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Profiles.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateDoctorAsync([FromBody] CreateDoctorRequest doctorRequest)
        {
            await _doctorService.CreateDoctorAsync(doctorRequest.FirstName, doctorRequest.LastName, doctorRequest.MiddleName, 
                doctorRequest.CabinetNumber, doctorRequest.DateOfBirth, doctorRequest.Email, doctorRequest.SpecializationId,
                doctorRequest.OfficeId, doctorRequest.CareerStartYear, doctorRequest.Status, doctorRequest.PhotoId);

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetAllDoctorsAsync()
        {
            return Ok(await _doctorService.GetAllDoctorsAsync());
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetDoctorByIdAsync(Guid id)
        {
            return Ok(await _doctorService.GetDoctorByIdAsync(id));
        }

        [AllowAnonymous]
        [HttpGet("at-work")]
        public async Task<ActionResult> GetAllDoctorsAtWorkAsync()
        {
            return Ok(await _doctorService.GetAllDoctorsAtWorkAsync());
        }

        [HttpGet("doctor-by-account-id-from-token")]
        public async Task<ActionResult> GetDoctorByAccountIdFromTokenAsync()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            return Ok(await _doctorService.GetDoctorByAccountIdFromTokenAsync(token));
        }

        [AllowAnonymous]
        [HttpGet("status-by-account-id/{accountId:guid}")]
        public async Task<ActionResult> GetDoctorStatusByAccountIdAsync(Guid accountId)
        {
            var account = await _doctorService.GetDoctorByAccountIdAsync(accountId);
            return Ok(account.Status);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateDoctorAsync(Guid id, [FromBody] UpdateDoctorRequest doctorRequest)
        {
            await _doctorService.UpdateDoctorAsync(id, doctorRequest.FirstName, doctorRequest.LastName, doctorRequest.MiddleName,
                doctorRequest.CabinetNumber, doctorRequest.DateOfBirth, doctorRequest.SpecializationId,
                doctorRequest.OfficeId, doctorRequest.CareerStartYear, doctorRequest.Status, doctorRequest.PhotoId);

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteDoctorAsync(Guid id)
        {
            await _doctorService.DeleteDoctorAsync(id);

            return Ok();
        }
    }
}
