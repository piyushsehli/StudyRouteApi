using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudyRouteApi.Models
{
    public class ProgramForCreationDto
    {
        [Required(ErrorMessage = "You should provide an Id.")]
        public string Id { get; set; }

        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [MaxLength(20)]
        public string ProgramType { get; set; }

        [MaxLength(80)]
        public string CampusName { get; set; }

        [MaxLength(100)]
        public string CampusLocation { get; set; }

        public float Fees { get; set; }

        [MaxLength(150)]
        public string AdmissionRequirements { get; set; }

        [MaxLength(10)]
        public string SummerBreak { get; set; }

        [MaxLength(10)]
        public string Coop { get; set; }

        public int Credits { get; set; }

        public int NumberOfCourses { get; set; }
    }
}
