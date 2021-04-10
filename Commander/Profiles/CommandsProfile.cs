using AutoMapper;
using DTOs;
using Models;

namespace Commander.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            CreateMap<Command, CommandReadDTO>();
        }
    }
}