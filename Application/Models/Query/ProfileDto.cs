using AutoMapper;
using SceletonAPI.Domain.Entities;
using SceletonAPI.Application.Interfaces.Mappings;

namespace SceletonAPI.Application.Models.Query
{
    public class ProfileDto : IHaveCustomMapping
    {
        public int Id;
        public ProfileData Profile { set; get;}

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<User, ProfileDto>()
                .ForMember(uDto => uDto.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(uDto => uDto.Profile, opt => opt.MapFrom(u => new ProfileData
                {
                    Email = u.Email,
                    Phone = u.Phone,
                    Name = u.Name,
                }));
        }
    }

    public class ProfileData
    {
        public string Name { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string EmployeeCode {set;get;}
    }
}
