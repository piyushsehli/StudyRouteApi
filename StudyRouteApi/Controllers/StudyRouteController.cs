using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyRouteApi.Models;
using StudyRouteApi.Services;
using StudyRouteLibrary.Entities;

namespace StudyRouteApi.Controllers
{
    [Route("api/colleges")]
    [ApiController]
    public class StudyRouteController : ControllerBase
    {
        private IStudyRouteRepository _studyRouteRepository;
        private readonly IMapper _mapper;

        public StudyRouteController(IStudyRouteRepository studyRouteRepository, IMapper mapper)
        {
            _studyRouteRepository = studyRouteRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Colleges>> GetColleges()
        {
            var collegesEntities = await _studyRouteRepository.GetColleges();

            var results = _mapper.Map<IEnumerable<CollegeWithoutProgramsDto>>(collegesEntities);

            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Colleges>> GetCollegeById(int id, bool includePrograms = false)
        {
            var college = await _studyRouteRepository.GetCollegeById(id, includePrograms);

            if (college == null)
            {
                return NotFound();
            }

            if (includePrograms)
            {
                var collegeResult = _mapper.Map<CollegeDto>(college);
                return Ok(collegeResult);
            }

            var collegeWithoutProgramsResult = _mapper.Map<CollegeWithoutProgramsDto>(college);
            return Ok(collegeWithoutProgramsResult);
        }

        [HttpPost]
        public async Task<ActionResult<CollegeDto>> CreateCollege([FromBody]CollegeDto college)
        {
            if (college == null) return BadRequest();

            if (college.Description == college.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var finalCollege = _mapper.Map<Colleges>(college);

            await _studyRouteRepository.AddCollege(finalCollege);


            if (!await _studyRouteRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var createdProgramReturn = _mapper.Map<CollegeDto>(finalCollege);

            return CreatedAtAction("GetCollegeById", new { id = createdProgramReturn.Id }, createdProgramReturn);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCollege(int id, [FromBody] CollegeForUpdateDto college)
        {
            if (college == null) return BadRequest();

            if (college.Description == college.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            Colleges oldCollegeEntity = await _studyRouteRepository.GetCollegeById(id, false);

            if (oldCollegeEntity == null) return NotFound();

            _mapper.Map(college, oldCollegeEntity);


            if (!await _studyRouteRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCollege(int id)
        {
            if (!await _studyRouteRepository.CollegeExists(id)) return NotFound();

            Colleges collegeToDelete = await _studyRouteRepository.GetCollegeById(id, false);
            if (collegeToDelete == null) return NotFound();

            _studyRouteRepository.DeleteCollege(collegeToDelete);

            if (!await _studyRouteRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
    }
}