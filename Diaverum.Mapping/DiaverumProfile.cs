using AutoMapper;
using Diaverum.Data;
using Diaverum.Domain;

namespace Diaverum.Mapping
{
    public class DiaverumProfile : Profile
    {
        public DiaverumProfile()
        {
            CreateMap<DiaverumItem, DiaverumItemDTO>()
                .ForMember(to => to.RequredStringValue, c => c.MapFrom(from => from.Text))
                .ForMember(to => to.OptionalStringValue, c => c.MapFrom(from => from.TextDetails))
                .ForMember(to => to.DateValue, c => c.MapFrom(from => from.EventDate))
                .ReverseMap();
        }
    }
}
