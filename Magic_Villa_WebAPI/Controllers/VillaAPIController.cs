using Microsoft.AspNetCore.Mvc;
using Magic_Villa_WebAPI.Domain.Models.Dtos;
using Magic_Villa_WebAPI.Data;

namespace Magic_Villa_WebAPI.Controllers
{
    //[Route("api/[controller]")] basically substitute the controller name 
    [Route("api/VillaAPI")] //setting the basic rout to this controller API
    [ApiController] //controller attribute to annotate as api controller class
    //Inheriting the controller base calss in order to get functionalities of controller class
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<VillaDto> GetAllVillas()
        {
            return VillaDataClass.villasList;
        }


        [HttpGet("{id:int}",Name = "GetVilla")] //we can pass route name too
        [ProducesResponseType(StatusCodes.Status200OK)] //documenting the response type
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVillaById(int id)
        {
            if(id == 0 || id == null)
            {
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

        public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villa) 
        {
            if (villa == null) { 
                return BadRequest();
            }

            if(villa.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            villa.Id = VillaDataClass.villasList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;

            VillaDataClass.villasList.Add(villa);

            return CreatedAtRoute("GetVilla",new {id = villa.Id},villa); // Name,param,whole object
        }

    }
}
