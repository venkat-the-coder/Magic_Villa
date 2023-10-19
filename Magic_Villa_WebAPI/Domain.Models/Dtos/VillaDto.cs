using System.ComponentModel.DataAnnotations;

namespace Magic_Villa_WebAPI.Domain.Models.Dtos
{
    public class VillaDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)] //adding data annotations to fix constaraints 
        public string Name { get; set; }

        public int Sqft { get; set; }   

    }
}
