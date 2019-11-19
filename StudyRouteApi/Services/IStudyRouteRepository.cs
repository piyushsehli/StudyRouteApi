using StudyRouteLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyRouteApi.Services
{
    public interface IStudyRouteRepository
    {
        Task<bool> CollegeExists(int CollegeId);
        Task<IEnumerable<Colleges>> GetColleges();
        Task<Colleges> GetCollegeById(int CollegeId, bool IncludePrograms);
        Task<IEnumerable<Programs>> GetProgramsForCollege(int CollegeId);
        Task<Programs> GetProgramForCollege(int CollegeId, int ProgramId);
        Task AddProgramForCollege(int CollegeId, Programs Program);
        Task AddCollege(Colleges College);
        void DeleteProgram(Programs Program);
        void DeleteCollege(Colleges College);
        Task<bool> Save();
    }
}
