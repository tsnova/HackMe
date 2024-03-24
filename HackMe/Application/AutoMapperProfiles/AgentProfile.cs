using AutoMapper;
using Domain = HackMe.Application.Models;

namespace HackMe.Application.AutoMapperProfiles
{
    public class AgentProfile : Profile
    {
        public AgentProfile()
        {
            this.CreateMap<Domain.Agent, HackMe.Models.AgentViewModel>();
            this.CreateMap<Domain.News, HackMe.Models.NewsViewModel>();
        }
    }
}
