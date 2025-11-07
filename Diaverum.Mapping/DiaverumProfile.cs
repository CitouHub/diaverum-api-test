using AutoMapper;
using Diaverum.Common.Extension;
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

            CreateMap<string[], LabResult>()
                .ForMember(to => to.ClinicNo, c => c.MapFrom(from => from[0]))
                .ForMember(to => to.Barcode, c => c.MapFrom(from => from[1]))
                .ForMember(to => to.PatientId, c => c.MapFrom(from => int.Parse(from[2])))
                .ForMember(to => to.PatientName, c => c.MapFrom(from => from[3]))
                .ForMember(to => to.Dbo, c => c.MapFrom(from => DateOnly.Parse(from[4])))
                .ForMember(to => to.Gender, c => c.MapFrom(from => from[5]))
                .ForMember(to => to.CollentionDate, c => c.MapFrom(from => DateOnly.Parse(from[6])))
                .ForMember(to => to.CollentionTime, c => c.MapFrom(from => TimeOnly.Parse(from[7])))
                .ForMember(to => to.TestCode, c => c.MapFrom(from => from[8]))
                .ForMember(to => to.TestName, c => c.MapFrom(from => from[9]))
                .ForMember(to => to.Result, c => c.MapFrom(from => from[10].ToDouble()))
                .ForMember(to => to.Unit, c => c.MapFrom(from => from[11].Length > 0 ? from[11] : null))
                .ForMember(to => to.RefrangeLow, c => c.MapFrom(from => from[12].Length > 0 ? from[12] : null))
                .ForMember(to => to.RefrangeHigh, c => c.MapFrom(from => from[13].Length > 0 ? from[13] : null))
                .ForMember(to => to.Note, c => c.MapFrom(from => from[14].Length > 0 ? from[14] : null))
                .ForMember(to => to.NonSpecRefs, c => c.MapFrom(from => from[15].Length > 0 ? from[15] : null));

            CreateMap<LabResult, LabResultDTO>();
        }
    }
}
