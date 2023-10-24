using AutoMapper;
using Magic_Villa_WebAPI.Domain.Models;
using Magic_Villa_WebAPI.Domain.Models.Dtos;
using Magic_Villa_WebAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Magic_Villa_WebAPI.Controllers
{

    [Route("api/VillaNumber")]
    [ApiController]
    public class VillaNumberController : ControllerBase
    {
        private readonly IVillaNumberRepository _villaNumberRepository;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public VillaNumberController(IVillaNumberRepository villaNumberRepository, IMapper mapper)
        {
            _villaNumberRepository = villaNumberRepository;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillaNumbers()
        {
            try
            {
                IEnumerable<VillaNumberModel> villaNumbers = await _villaNumberRepository.GetAll();
                _response.APIContent = _mapper.Map<IEnumerable<VillaNumberDto>>(villaNumbers);
                _response.StatusCode = HttpStatusCode.OK;
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ex.ToString());
                return _response;
            }
        }

        [HttpGet("{villaNo:int}",Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillaById(int villaNo)
        {
            try
            {
                if (villaNo == null)
                {
                    return BadRequest();
                }
                VillaNumberModel villaNumber = await _villaNumberRepository.GetById(x => x.VillaNo == villaNo);


                if (villaNumber == null)
                {
                    return NotFound();
                }
                _response.APIContent = _mapper.Map<VillaNumberDto>(villaNumber);
                _response.StatusCode = HttpStatusCode.OK;
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ex.ToString());
                return _response;
            }
        }

        [HttpPost]

        public async Task<ActionResult<APIResponse>> CreateVillNumber( [FromBody] VillaNumberCreateDto villaNumber)
        {
            try
            {
                if (villaNumber == null)
                {
                    return BadRequest();
                }
                VillaNumberModel villaNo = _mapper.Map<VillaNumberModel>(villaNumber);
                await _villaNumberRepository.Create(villaNo);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVillaNumber", new { id = villaNumber.VillaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ex.ToString());
                return _response;
            }

        }

    }
}
