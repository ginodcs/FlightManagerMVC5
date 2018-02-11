using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ARQ.Maqueta.Entities;
using ARQ.Maqueta.Presentation.Mvc.ViewModels;
using ARQ.Maqueta.Presentation.Mvc.Extensions.Helpers;

namespace ARQ.Maqueta.Presentation.Mvc
{
    public static partial class EntityMappingsConfig
    {
        public static void CreateMaps()
        {
            Mapper.CreateMap<FlightViewModel, Flight>()
                .ForMember(m => m.Source, opt => opt.Ignore())
                .ForMember(m => m.Destination, opt => opt.Ignore());
            Mapper.CreateMap<Flight, FlightViewModel>()
                 .ForMember(m => m.AiportList, opt => opt.Ignore());

            Mapper.CreateMap<CreateFlightViewModel, Flight>()
                .ForMember(m => m.Source, opt => opt.Ignore())
                .ForMember(m => m.Destination, opt => opt.Ignore());
            Mapper.CreateMap<Flight, CreateFlightViewModel>()
                .ForMember(m => m.AiportList, opt => opt.Ignore());

            Mapper.CreateMap<EditFlightViewModel, Flight>()
                .ForMember(m => m.Source, opt => opt.Ignore())
                .ForMember(m => m.Destination, opt => opt.Ignore());
            Mapper.CreateMap<Flight, EditFlightViewModel>()
                .ForMember(m => m.AiportList, opt => opt.Ignore());

            Mapper.CreateMap<DetailsFlightViewModel, Flight>()
                .ForMember(m => m.Source, opt => opt.Ignore())
                .ForMember(m => m.Destination, opt => opt.Ignore());
            Mapper.CreateMap<Flight, DetailsFlightViewModel>()
                .ForMember(m => m.AiportList, opt => opt.Ignore());

            Mapper.CreateMap<AiportViewModel, Aiport>();
            Mapper.CreateMap<Aiport, AiportViewModel>();

            Mapper.AssertConfigurationIsValid(); //Validate mappings
        }
    }
}