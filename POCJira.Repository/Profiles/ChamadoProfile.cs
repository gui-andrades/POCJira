using AutoMapper;
using POCJira.Repository.Entities;
using POCJira.Shared.Models;

namespace POCJira.Repository.Profiles
{
    public class ChamadoProfile : Profile
    {
        public ChamadoProfile()
        {
            CreateMap<ChamadoEntity, ChamadoModel>();
        }
    }
}
