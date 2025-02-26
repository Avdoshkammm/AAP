using AAP.Application.DTO;
using AAP.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAP.Application.Mapping
{
    public class MappintProfile : Profile
    {
        public MappintProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.uID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.uFirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.uLastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.uPathronomic, opt => opt.MapFrom(src => src.Pathronomic))
                .ForMember(dest => dest.uUserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.uEmail, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.uPassword, opt => opt.MapFrom(src => src.PasswordHash))
                .ReverseMap();
        }
    }
}
