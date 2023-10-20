using System.Reflection;
using AutoMapper;
using SceletonAPI.Application.Interfaces;
using SceletonAPI.Application.Misc;

namespace SceletonAPI.Application.Infrastructures.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public IDBContext _context { set; get; }
        public Utils _utils { set; get; }
        public AutoMapperProfile(IDBContext context, Utils utils)
        {
            _context = context;
            _utils = utils;
            
            LoadStandardMappings();
            LoadCustomMappings();
            LoadConverters();
        }

        private void LoadConverters()
        {

        }

        private void LoadStandardMappings()
        {
            var mapsFrom = MapperProfileHelper.LoadStandardMappings(Assembly.GetExecutingAssembly());
            foreach(var map in mapsFrom)
            {
                CreateMap(map.Source, map.Destination).ReverseMap();
            }
        }

        private void LoadCustomMappings()
        {
            var mapsFrom = MapperProfileHelper.LoadCustomMappings(Assembly.GetExecutingAssembly());
            foreach(var map in mapsFrom)
            {
                map.CreateMappings(this);
            }
        }

        public string GetFullUrl(string path)
        {
            return _utils.GetValidUrl(path);
        }
    }
}
