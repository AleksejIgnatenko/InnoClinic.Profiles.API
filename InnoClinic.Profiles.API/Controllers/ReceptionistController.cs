﻿using InnoClinic.Profiles.Application.Services;
using InnoClinic.Profiles.Core.Models.ReceptionistModels;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Profiles.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceptionistController : ControllerBase
    {
        private readonly IReceptionistService _receptionistService;

        public ReceptionistController(IReceptionistService receptionistService)
        {
            _receptionistService = receptionistService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateReceptionistAsync(CreateReceptionistRequest receptionistRequest)
        {
            await _receptionistService.CreateReceptionistAsync(receptionistRequest.FirstName, receptionistRequest.LastName,
                receptionistRequest.MiddleName, receptionistRequest.Email, receptionistRequest.Status, receptionistRequest.OfficeId);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllReceptionistsAsync()
        {
            return Ok(await _receptionistService.GetAllReceptionistsAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetReceptionistByIdAsync(Guid id)
        {
            return Ok(await _receptionistService.GetReceptionistByIdAsync(id));
        }

        [HttpGet("receptionist-by-account-id-from-token")]
        public async Task<ActionResult> GetReceptionistByAccountIdFromTokenAsync()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            return Ok(await _receptionistService.GetReceptionistByAccountIdFromTokenAsync(token));
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateReceptionistAsync(Guid id, UpdateReceptionistRequest updateReceptionist)
        {
            await _receptionistService.UpdateReceptionistAsync(id, updateReceptionist.FirstName, updateReceptionist.LastName,
                updateReceptionist.MiddleName, updateReceptionist.Status, updateReceptionist.OfficeId); 

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteReceptionistAsync(Guid id)
        {
            await _receptionistService.DeleteReceptionistAsync(id);

            return Ok();
        }
    }
}
