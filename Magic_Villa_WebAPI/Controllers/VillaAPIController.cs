using Microsoft.AspNetCore.Mvc;
using Magic_Villa_WebAPI.Domain.Models.Dtos;
using Magic_Villa_WebAPI.Data;
using Microsoft.AspNetCore.JsonPatch;

namespace Magic_Villa_WebAPI.Controllers
{
    //[Route("api/[controller]")] basically substitute the controller name 
    [Route("api/VillaAPI")] //setting the basic rout to this controller API
    [ApiController]//controller attribute to annotate as api controller class
    //Inheriting the controller base calss in order to get functionalities of controller class
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogger<VillaAPIController> _logger;

        public VillaAPIController(ILogger<VillaAPIController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<VillaDto> GetAllVillas()
        {
            _logger.LogInformation("Getting All Villas....");
            return VillaDataClass.villasList;
        }


        [HttpGet("{id:int}", Name = "GetVilla")] //we can pass route name too
        [ProducesResponseType(StatusCodes.Status200OK)] //documenting the response type
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVillaById(int id)
        {
            if (id == 0 || id == null)
            {
                _logger.LogError("Badrequest...");
                Console.BackgroundColor = ConsoleColor.Green;
                return BadRequest("please enter valid ID");
            }
            else
            {
                var villa = VillaDataClass.villasList.FirstOrDefault(x => x.Id == id);
                if (villa == null)
                {
                    return NotFound("Data not Found");
                }
                return villa;
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villa)
        {
            //default model validation will be checked 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Custom Validation
            if (VillaDataClass.villasList.FirstOrDefault(x => x.Name.ToLower() == villa.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", "Villa Name should be unique");
                return BadRequest(ModelState);
            }

            if (villa == null) {
                return BadRequest();
            }

            if (villa.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            villa.Id = VillaDataClass.villasList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;

            VillaDataClass.villasList.Add(villa);

            return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa); // Name,param,whole object
        }

        [HttpDelete("{Name}")]

        public ActionResult<VillaDto> DeleteVillaByName(string Name)
        {
            if (Name == null)
            {
                return BadRequest();
            }

            var villa = VillaDataClass.villasList.Find(x => x.Name.ToLower() == Name.ToLower());

            if (villa == null)
            {
                return NotFound();
            }

            VillaDataClass.villasList.Remove(villa);
            return Ok($"{villa.Name} is deleted sucessfully");
        }


        [HttpPut("{id:int}")]

        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villa) //IactionResult used to return no content
        {
            if (id == null || id != villa.Id)
            {
                return BadRequest();
            }

            var villaFound = VillaDataClass.villasList.FirstOrDefault(x => x.Id == id);

            if (villaFound == null)
            {
                return NotFound();
            }

            villaFound.Name = villa.Name;
            villaFound.Sqft = villa.Sqft;

            return NoContent();
        }


        [HttpPatch("{id:int}")]

        public IActionResult PartialUpdateVilla(int id , JsonPatchDocument<VillaDto> patchVilla)
        {
            if (id == null || patchVilla == null)
            {
                return BadRequest();
            }

            var villaFound = VillaDataClass.villasList.FirstOrDefault(x => x.Id == id);

            if(villaFound  == null)
            {
                return BadRequest();    
            }

            patchVilla.ApplyTo(villaFound,ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }
            return NoContent();
        }

    }
}
