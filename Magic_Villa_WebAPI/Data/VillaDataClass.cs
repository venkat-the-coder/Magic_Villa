using Magic_Villa_WebAPI.Domain.Models.Dtos;

namespace Magic_Villa_WebAPI.Data
{
    public class VillaDataClass
    {
        public static List<VillaDto> villasList = new List<VillaDto>()
        {
                new VillaDto {Id = 101 , Name = "Villa1"},
                new VillaDto {Id = 102 , Name = "Villa2"},
                new VillaDto {Id = 103 , Name = "Villa3"},
                new VillaDto {Id = 104 , Name = "Villa4"},
                new VillaDto {Id = 105 , Name = "Villa5"},
                new VillaDto {Id = 106 , Name = "Villa6"}
        };
    }
}
