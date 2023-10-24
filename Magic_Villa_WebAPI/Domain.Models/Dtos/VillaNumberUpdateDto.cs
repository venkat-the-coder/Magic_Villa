using System.ComponentModel.DataAnnotations;

namespace Magic_Villa_WebAPI.Domain.Models.Dtos
{
    public class VillaNumberUpdateDto
    {
        [Required]
        public int VillaNo { get; set; }
        public string SpecialDetails { get; set; }
    }
}
