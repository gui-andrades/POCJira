using AutoMapper;
using POCJira.Shared.Models;
using POCJira.WebApi.DTOs.Requests;
using POCJira.WebApi.DTOs.Responses;

namespace POCJira.WebApi.Profiles
{
    public class SqlProfile : Profile
    {
        public SqlProfile()
        {
            CreateMap<ChamadoModel, ChamadoDto>();
            CreateMap<UpdateSqlRequest, UpdateSqlModel>();
            CreateMap<UserRequest, UserModel>();
            CreateMap<IssueRequest, IssueModel>();
            CreateMap<FieldsRequest, FieldsModel>();
            CreateMap<StatusRequest, StatusModel>();
            CreateMap<ChangelogRequest, ChangelogModel>();
            CreateMap<ChangelogItemRequest, ChangelogItemModel>();
        }
    }
}
