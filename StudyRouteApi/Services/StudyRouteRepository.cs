using Microsoft.EntityFrameworkCore;
using StudyRouteLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyRouteApi.Services
{
    public class StudyRouteRepository : IStudyRouteRepository
    {
        private StudyRouteDbContext _context;
        public StudyRouteRepository(StudyRouteDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CollegeExists(int CollegeId)
        {
            return await _context.Colleges.AnyAsync<Colleges>(c => c.Id == CollegeId);
        }

        public async Task<IEnumerable<Colleges>> GetColleges()
        {
            var result = _context.Colleges.OrderBy(c => c.Name);
            return await result.ToListAsync();
        }

        public async Task<Colleges> GetCollegeById(int CollegeId, bool IncludePrograms)
        {

            IQueryable<Colleges> result;

            if (IncludePrograms)
            {
                result = _context.Colleges.Include(c => c.Programs)
                    .Where(c => c.Id == CollegeId);
            }
            else result = _context.Colleges.Where(c => c.Id == CollegeId);

            return await result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Programs>> GetProgramsForCollege(int CollegeId)
        {
            IQueryable<Programs> result = _context.Programs.Where(p => p.CollegeId == CollegeId);
            return await result.ToListAsync();
        }

        public async Task<Programs> GetProgramForCollege(int CollegeId, int ProgramId)
        {
            IQueryable<Programs> result = _context.Programs.Where(p => p.CollegeId == CollegeId && p.Id == ProgramId);
            return await result.FirstOrDefaultAsync();
        }

        public async Task AddProgramForCollege(int CollegeId, Programs program)
        {
            var College = await GetCollegeById(CollegeId, false);
            College.Programs.Add(program);
        }

        public async Task AddCollege(Colleges college)
        {
            await _context.Colleges.AddAsync(college);
        }

        public void DeleteProgram(Programs program)
        {
            _context.Programs.Remove(program);
        }

        public void DeleteCollege(Colleges college)
        {
            _context.Colleges.Remove(college);
        }

        public async Task<bool> Save()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
