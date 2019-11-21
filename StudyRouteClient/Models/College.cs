using System;
using System.Collections.Generic;

namespace StudyRouteLibrary.Entities
{
    public partial class College
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Ratings { get; set; }

        public int? NumberOfPrograms { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
        
    }
}
