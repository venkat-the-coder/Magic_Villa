using Microsoft.AspNetCore.Mvc;
using Magic_Villa_WebAPI.Domain.Models.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Magic_Villa_WebAPI.DB_Context;
using Magic_Villa_WebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Magic_Villa_WebAPI.Repository.IRepository;

namespace Magic_Villa_WebAPI.Controllers
{
    //[Route("api/[controller]")] basically substitute the controller name 
    [Route("api/VillaAPI")] //setting the basic rout to this controller API
    [ApiController]//controller attribute to annotate as api controller class
    //Inheriting the controller base calss in order to get functionalities of controller class
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogger<VillaAPIController> _logger;
        private readonly IMapper _mapper;
        private readonly IVillaRepository _villaRepository;

        public VillaAPIController(ILogger<VillaAPIController> logger, VillaDBContext context, IMapper mapper,IVillaRepository villaRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _villaRepository = villaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetAllVillas()
        {
            _logger.LogInformation("Getting All Villas....");
            IEnumerable<VillaModelClass> villas = await  _villaRepository.GetAll();
            return Ok(_mapper.Map<VillaDto>(villas));
        }


        [HttpGet("{id:int}", Name = "GetVilla")] //we can pass route name too
        [ProducesResponseType(StatusCodes.Status200OK)] //documenting the response type
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDto>> GetVillaById(int id)
        {
            if (id == 0 || id == null)
            {
                _logger.LogError("Badrequest...");
                Console.BackgroundColor = ConsoleColor.Green;
                return BadRequest("please enter valid ID");
            }
            else
            {
                VillaModelClass villa = await _villaRepository.GetById(x => x.Id == id);
                if (villa == null)
                {
                    return NotFound("Data not Found");
                }
                return Ok(_mapper.Map<VillaDto>(villa));
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody] VillaCreateDto villa)
        {
            //default model validation will be checked 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Custom Validation
            if(await _villaRepository.GetAll(x => x.Name.ToLower() == villa.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", "Villa Name should be unique");
                return BadRequest(ModelState);
            }

            if (villa == null) {
                return BadRequest();
            }

            VillaModelClass newVilla =  _mapper.Map<VillaModelClass>(villa);
            await _villaRepository.Create(newVilla);

            return CreatedAtRoute("GetVilla", new { id = newVilla.Id }, villa); // Name,param,whole object
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult<VillaDto>> DeleteVillaByName(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var villa = await _villaRepository.GetById(x => x.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            _villaRepository.Remove(villa);

            return Ok($"{villa.Name} is deleted sucessfully");
        }


        [HttpPut("{id:int}")]

        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto villa) //IactionResult used to return no content
        {
            if (id == null || id != villa.Id)
            {
                return BadRequest();
            }

            var villaFound = _villaRepository.GetAll(x => x.Id == id);

            if (villaFound == null)
            {
                return NotFound();
            }

            VillaModelClass newVilla = _mapper.Map<VillaModelClass>(villa);

            await _villaRepository.Update(newVilla);

            return NoContent();
        }


        [HttpPatch("{id:int}")]

        public async  Task<IActionResult> PartialUpdateVilla(int id , JsonPatchDocument<VillaUpdateDto> patchVilla)
        {
            if (id == null || patchVilla == null)
            {
                return BadRequest();
            }

            var villaFound = _villaRepository.GetById(x => x.Id == id , true);

            if(villaFound  == null)
            {
                return BadRequest();    
            }

            VillaUpdateDto newVillaDto = _mapper.Map<VillaUpdateDto>(villaFound);

            patchVilla.ApplyTo(newVillaDto, ModelState);

            VillaModelClass newVilla = _mapper.Map<VillaModelClass>(newVillaDto);

            _villaRepository.Update(newVilla);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }

            return NoContent();
        }

    }
}
