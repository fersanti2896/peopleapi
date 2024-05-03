using AutoMapper;
using PeopleAPI.DTOs;
using PeopleAPI.Models;

namespace PeopleAPI.Utilities {
    public class AutoMapperProfiles : Profile {
        public AutoMapperProfiles() {
            CreateMap<PeopleCreateDTO, People>();
            CreateMap<People, PeopleDTO>();
        }
    }
}
