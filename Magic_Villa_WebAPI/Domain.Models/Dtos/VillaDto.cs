using System.ComponentModel.DataAnnotations;

namespace Magic_Villa_WebAPI.Domain.Models.Dtos
{
    public class VillaDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)] //adding data annotations to fix constaraints 
        public string Name { get; set; }
        public string Details { get; set; }
        [Required]
        public double Rate { get; set; }

        public int Sqft { get; set; }

        public int Occupancy { get; set; }
        public string ImageURL { get; set; }

        public string Amenity { get; set; }

    }
}
