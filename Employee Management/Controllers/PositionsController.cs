using AutoMapper;
using Employee_Management.Data;
using Employee_Management.Dtos;
using Employee_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Employee_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PositionsController : Controller
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IMapper _mapper;

        public PositionsController(IRepositoryWrapper repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPositions() 
        { 
            var postions = await _repo.Position.GetAllAsync();
            if (postions == null)
                return NotFound(); 
            return Ok(postions.Select(_mapper.Map<PositionGetDto>));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPosition(int id)
        {
            var position = await _repo.Position.GetByIdAsync(id); 
            if(position == null)
                return NotFound($"Position with {id} doesn't exist.");
            return Ok(_mapper.Map<PositionGetDto>(position));
        }

        [HttpPost]
        public async Task<IActionResult> AddPosition(PositionCreateDto position)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                await _repo.Position.CreateAsync(_mapper.Map<Position>(position));
                await _repo.SaveChangesAsync();
                return CreatedAtAction(nameof(AddPosition), "Employee added successfully"); 
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong. Please try again later.");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePosition(PositionCreateDto position, int id)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var pos = await _repo.Position.GetByIdAsync(id);
            if(pos != null)
            {
                try
                {
                    var positionMap = _mapper.Map<Position>(position);
                    positionMap.Id = pos.Id;    
                    _repo.Position.Update(positionMap); 
                    await _repo.SaveChangesAsync();
                    return Ok("Updated successfully");
                }
                catch (Exception)
                {
                    return StatusCode(500, "Something went wrong. Please try again later.");
                }
               
            }
            return NotFound();    
        }
    }
}
