using AutoMapper;

namespace SceletonAPI.Application.Interfaces.Mappings {
    
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile profile);
    }
}