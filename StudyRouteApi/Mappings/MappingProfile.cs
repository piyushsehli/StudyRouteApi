using AutoMapper;
using StudyRouteLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudyRouteApi.Models;
namespace StudyRouteApi.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {
            CreateMap<Colleges, CollegeWithoutProgramsDto>();
            CreateMap<Colleges, CollegeDto>();
            CreateMap<CollegeForUpdateDto, Colleges>();
            CreateMap<Programs, ProgramDto>();
            CreateMap<ProgramForCreationDto, Programs>();
            CreateMap<ProgramForUpdateDto, Programs>();
        }
    }
}