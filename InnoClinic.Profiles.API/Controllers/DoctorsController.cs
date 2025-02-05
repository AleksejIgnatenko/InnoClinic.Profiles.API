using InnoClinic.Profiles.API.Contracts;
using InnoClinic.Profiles.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Profiles.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateDoctorAsync(DoctorRequest doctorRequest)
        {
            await _doctorService.CreateDoctorAsync(doctorRequest.FirstName, doctorRequest.LastName, doctorRequest.MiddleName, 
                doctorRequest.CabinetNumber, doctorRequest.DateOfBirth, doctorRequest.AccountId, doctorRequest.SpecializationId,
                doctorRequest.OfficeId, doctorRequest.CareerStartYear, doctorRequest.Status);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllDoctorsAsync()
        {
            return Ok(await _doctorService.GetAllDoctorsAsync());
        }


        [HttpGet("at-work")]
        public async Task<ActionResult> GetAllDoctorsAtWorkAsync()
        {
            return Ok(await _doctorService.GetAllDoctorsAtWorkAsync());
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateDoctorAsync(Guid id, DoctorRequest doctorRequest)
        {
            await _doctorService.UpdateDoctorAsync(id, doctorRequest.FirstName, doctorRequest.LastName, doctorRequest.MiddleName,
                doctorRequest.CabinetNumber, doctorRequest.DateOfBirth, doctorRequest.AccountId, doctorRequest.SpecializationId,
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
