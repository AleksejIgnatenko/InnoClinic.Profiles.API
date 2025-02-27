using InnoClinic.Profiles.Application.Services;
using InnoClinic.Profiles.Core.Models.DoctorModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Profiles.API.Controllers
{
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
        public async Task<ActionResult> CreateDoctorAsync(CreateDoctorRequest doctorRequest)
        {
            await _doctorService.CreateDoctorAsync(doctorRequest.FirstName, doctorRequest.LastName, doctorRequest.MiddleName, 
                doctorRequest.CabinetNumber, doctorRequest.DateOfBirth, doctorRequest.Email, doctorRequest.SpecializationId,
                doctorRequest.OfficeId, doctorRequest.CareerStartYear, doctorRequest.Status);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllDoctorsAsync()
        {
            return Ok(await _doctorService.GetAllDoctorsAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetDoctorByIdAsync(Guid id)
        {
            return Ok(await _doctorService.GetDoctorByIdAsync(id));
        }

        [HttpGet("at-work")]
        public async Task<ActionResult> GetAllDoctorsAtWorkAsync()
        {
            return Ok(await _doctorService.GetAllDoctorsAtWorkAsync());
        }

        [Authorize]
        [HttpGet("doctor-by-account-id-from-token")]
        public async Task<ActionResult> GetDoctorByAccountIdFromTokenAsync()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            return Ok(await _doctorService.GetDoctorByAccountIdFromTokenAsync(token));
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateDoctorAsync(Guid id, UpdateDoctorRequest doctorRequest)
        {
            await _doctorService.UpdateDoctorAsync(id, doctorRequest.FirstName, doctorRequest.LastName, doctorRequest.MiddleName,
                doctorRequest.CabinetNumber, doctorRequest.DateOfBirth, doctorRequest.SpecializationId,
                doctorRequest.OfficeId, doctorRequest.CareerStartYear, doctorRequest.Status);

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
