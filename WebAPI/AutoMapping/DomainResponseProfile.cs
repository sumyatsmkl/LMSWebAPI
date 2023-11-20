using AutoMapper;
using DataAccess.DTO;
using Domain.Entities;

namespace WebAPI.AutoMapping
{
    public class DomainResponseProfile:Profile
    {
        public DomainResponseProfile()
        {
            CreateMap<TSystemCodeValue, SystemCodeValueDTO>();
            
        }
    }
}
