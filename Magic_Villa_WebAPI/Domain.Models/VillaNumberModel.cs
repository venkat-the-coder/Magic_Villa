using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magic_Villa_WebAPI.Domain.Models
{
    public class VillaNumberModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VillaNo { get; set; }    
        public string SpecialDetails { get; set; }  
        public DateTime CreatedDate { get; set; }   
        public DateTime LastModifiedDate { get; set; }
    }
}
