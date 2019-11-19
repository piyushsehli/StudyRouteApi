using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyRouteApi.Models
{
    public class CollegeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ratings { get; set; }
        public int NumberOfPrograms { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public ICollection<ProgramDto> Programs { get; set; }
        = new List<ProgramDto>();
    }
}
