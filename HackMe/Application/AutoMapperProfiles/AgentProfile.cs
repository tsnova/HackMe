using AutoMapper;
using HackMe.Application.Models.Dto;
using Domain = HackMe.Application.Models;

namespace HackMe.Application.AutoMapperProfiles
{
    public class AgentProfile : Profile
    {
        public AgentProfile()
        {
            this.CreateMap<Domain.Agent, HackMe.Models.AgentViewModel>();
            this.CreateMap<Domain.Mission, HackMe.Models.MissionViewModel>();
            this.CreateMap<ChallangeResultDetailsDto, HackMe.Models.ChallangeResultViewModel>();
        }
    }
}
