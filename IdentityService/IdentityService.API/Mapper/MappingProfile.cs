using AutoMapper;
using IdentityService.API.DTOs;
using IdentityServiceLibrary.Entities;
using IdentityServiceLibrary.Enums;

namespace IdentityService.API.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            // RegisterDto → Entity (string to enum)
            CreateMap<RegisterRequestDto, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore()) // ← hashed manually in service
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.Parse<RoleOption>(src.Role, true)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<StatusOption>(src.Status, true)));

            // Entity → ResponseDto (enum to string)
            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
