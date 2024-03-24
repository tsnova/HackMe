using AutoMapper;
using Models = HackMe.Application.Models;

namespace HackMe.Application.AutoMapperProfiles
{
    public class AgentProfile : Profile
    {
        public AgentProfile()
        {
            this.CreateMap<Models.Agent, HackMe.Models.AgentViewModel>();
            this.CreateMap<Models.News, HackMe.Models.NewsViewModel>();
        }
    }
}
