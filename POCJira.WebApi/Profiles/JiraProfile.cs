using AutoMapper;
using POCJira.Shared.Models;
using POCJira.WebApi.DTOs.Requests;

namespace POCJira.WebApi.Profiles
{
    public class JiraProfile : Profile
    {
        public JiraProfile()
        {
            CreateMap<CreateJiraCardRequest, CreateJiraCardModel>();
            CreateMap<UpdateJiraCardRequest, UpdateJiraCardModel>();
        }
    }
}
