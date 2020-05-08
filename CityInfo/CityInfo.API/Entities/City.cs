using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Entities
{
    public class City
    {
        [Key]// Is used in case the primary key is not taken by convention
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Indentity goes for generated on add. Computed for updates and add and none for none of those
        public int Id { get; set; }// Id by conventions is the primary Key in Entity Framework
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
        public ICollection<PointOfInterest> PointsOfInterest { get; set; } = new List<PointOfInterest>();
    }
}
