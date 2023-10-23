using AutoMapper;
using Magic_Villa_WebAPI.Domain.Models;
using Magic_Villa_WebAPI.Domain.Models.Dtos;

namespace Magic_Villa_WebAPI.Mapping.Config
{
    public class ModelMapConfig : Profile
    {
        public ModelMapConfig() {
            CreateMap<VillaDto, VillaModelClass>().ReverseMap();
            CreateMap<VillaCreateDto, VillaModelClass>().ReverseMap();
            CreateMap<VillaUpdateDto, VillaModelClass>().ReverseMap();
        }
    }
}
