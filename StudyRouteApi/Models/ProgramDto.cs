using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyRouteApi.Models
{
    public class ProgramDto
    {
        public int Id { get; set; }
        public int CollegeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProgramType { get; set; }
        public string CampusName { get; set; }
        public string CampusLocation { get; set; }
        public float Fees { get; set; }
        public string AdmissionRequirements { get; set; }
        public string SummerBreak { get; set; }
        public string Coop { get; set; }
        public int Credits { get; set; }
        public int NumberOfCourses { get; set; }
    }
}
