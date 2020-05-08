using CityInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
    public class PointOfInterestDto
    {
        [Key]// Is used in case the primary key is not taken by convention
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(150)]
        public string Description { get; internal set; }
    }
}
