using AutoMapper;
using GISApi.Data.GlobalEntities;
using GISApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISApi.Helpers
{

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddEditUserViewModel, ApplicationUser>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserName));

          

                  //CreateMap<ProjectViewModel, TblProject>()
            //    .ForMember(dest => dest.ProjectNumber, opt => opt.Condition(src => (src.ProjectId == 0))); //conditionally update project number only when creating project

            //CreateMap<Inspections, InspectionViewModel>();
            //CreateMap<InspectionViewModel, Inspections>()
            //    .ForMember(dest => dest.DateReceived, opt => opt.Ignore())
            //    .ForMember(dest => dest.TimeStamp, opt => opt.Ignore())
            //    .ForMember(dest => dest.ProjectNumber, opt => opt.Condition(src => (src.InspectionId == 0))); //conditionally update project number only when creating new inspection

            //CreateMap<GetIRsWithRevisions_Result, InspectionViewModel>();



            CreateMap<string, DateTime>().ConvertUsing<StringToDateTimeConverter>();
        }
    }
}

