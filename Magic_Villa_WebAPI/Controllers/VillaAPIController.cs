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


        [HttpGet("id")]
        public VillaDto GetVillaById(int id)
        {
            var villa = VillaDataClass.villasList.FirstOrDefault(x => x.Id == id);
            if(villa == null)
            {
                return null;
            }
            return villa;
        }

    }
}
