using AnalyzorBlazor.Models;
using AnalyzorBlazor.Models.Dto;
using AutoMapper;

namespace AnalyzorBlazor.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<CompReadOnlyDto, Component>()
                .ForMember(q => q.Time, d => d.MapFrom(map => map.Qty))
                .ReverseMap();
        }
    }
}
