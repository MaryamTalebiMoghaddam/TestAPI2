using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TestApi.Dto;
using TestApi.Models;
using TestApi.Services.IServices;

namespace TestApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ReservationController : ControllerBase
{
    private readonly IRepository<Reservation> _reservationRepository;
    private readonly IMapper _mapper;
    public ReservationController(IRepository<Reservation> reservationRepository,IMapper mapper)
    {
        _reservationRepository = reservationRepository; 
        _mapper = mapper;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllReservation()
    {
       var result= await _reservationRepository.GetAllReservation();
        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetReservationById(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
       var result = await _reservationRepository.GetReservationById(id);
        return Ok(result);

    }

    [HttpPost]
    public async Task<IActionResult> AddReservation([FromBody] ReservationDTO reservationDTO)
    {
        if (reservationDTO == null)
        {
            return BadRequest();
        }
        await _reservationRepository.AddReservation(reservationDTO);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteReservation(int id)
    {
        if (id==0)
        {
            return BadRequest();
        }
        await _reservationRepository.DeleteReservation(id);
        return Ok();
    }

    [HttpPatch("reservation/{id}")]
    public async Task<IActionResult> UpdateReservation(int id,[FromBody] JsonPatchDocument<ReservationDTO> jsonPatch)
    {
        

        Reservation reservationForUpdate = await _reservationRepository.GetReservationById(id);
        ReservationDTO reservationForUpdateDTO = _mapper.Map<ReservationDTO>(reservationForUpdate);

        if (reservationForUpdate == null)
        {
            return NotFound();
        }
        await _reservationRepository.UpdateReservation(id, reservationForUpdateDTO);
        jsonPatch.ApplyTo(reservationForUpdateDTO);
        return Ok();
    }
}
