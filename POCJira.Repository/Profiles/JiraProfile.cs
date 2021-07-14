using System;
using AutoMapper;
using POCJira.Repository.Responses;
using POCJira.Shared.Models;

namespace POCJira.Repository.Profiles
{
    public class JiraProfile : Profile
    {
        public JiraProfile()
        {
            CreateMap<JiraCardCreatedResponse, JiraCardCreatedModel>();
        }
    }
}
