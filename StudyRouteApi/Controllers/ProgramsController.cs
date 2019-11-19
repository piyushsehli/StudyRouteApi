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
    [Route("api/studyprograms")]
    [ApiController]
    public class ProgramsController : ControllerBase
    {
        private IStudyRouteRepository _studyRouteRepository;
        private readonly IMapper _mapper;

        public ProgramsController(IStudyRouteRepository studyRouteRepository, IMapper mapper)
        {
            _studyRouteRepository = studyRouteRepository;
            _mapper = mapper;
        }

        [HttpGet("{collegeId}/programs")]
        public async Task<ActionResult<Programs>> GetPrograms(int collegeId)
        {
            if (!(await _studyRouteRepository.CollegeExists(collegeId)))
            {
                return NotFound();
            }

            var programsForCity = await _studyRouteRepository.GetProgramsForCollege(collegeId);
            var programsForCityResults = _mapper.Map<IEnumerable<ProgramDto>>(programsForCity);

            return Ok(programsForCityResults);
        }

        [HttpGet("{collegeId}/programs/{id}")]
        public async Task<ActionResult<Programs>> GetProgramById(int collegeId, int id)
        {
            if (!await _studyRouteRepository.CollegeExists(collegeId))
            {
                return NotFound();
            }

            var program = await _studyRouteRepository.GetProgramForCollege(collegeId, id);

            if (program == null)
            {
                return NotFound();
            }

            var programResult = _mapper.Map<ProgramDto>(program);
            return Ok(programResult);
        }

        [HttpPost("{collegeId}/programs")]
        public async Task<ActionResult<ProgramDto>> CreateProgram(int collegeId, [FromBody]ProgramForCreationDto program)
        {
            if (program == null) return BadRequest();

            if (program.Description == program.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!await _studyRouteRepository.CollegeExists(collegeId)) return NotFound();

            var finalProgram = _mapper.Map<Programs>(program);

            await _studyRouteRepository.AddProgramForCollege(collegeId, finalProgram);


            if (!await _studyRouteRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var createdProgramReturn = _mapper.Map<ProgramDto>(finalProgram);

            return CreatedAtAction("GetProgramById", new { CollegeId = collegeId, id = createdProgramReturn.Id }, createdProgramReturn);
        }

        [HttpPut("{collegeId}/programs/{id}")]
        public async Task<ActionResult> UpdateProgram(int collegeId, int id, [FromBody] ProgramForUpdateDto program)
        {
            if (program == null) return BadRequest();

            if (program.Description == program.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!await _studyRouteRepository.CollegeExists(collegeId)) return NotFound();

            Programs oldProgramEntity = await _studyRouteRepository.GetProgramForCollege(collegeId, id);

            if (oldProgramEntity == null) return NotFound();

            _mapper.Map(program, oldProgramEntity);


            if (!await _studyRouteRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        [HttpDelete("{collegeId}/programs/{id}")]
        public async Task<ActionResult> DeleteProgram(int collegeId, int id)
        {
            if (!await _studyRouteRepository.CollegeExists(collegeId)) return NotFound();

            Programs programToDelete = await _studyRouteRepository.GetProgramForCollege(collegeId, id);
            if (programToDelete == null) return NotFound();

            _studyRouteRepository.DeleteProgram(programToDelete);

            if (!await _studyRouteRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
    }
}