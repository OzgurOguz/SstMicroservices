using AutoMapper;
using Sst.Contact.App.Dtos.ContactDtos;
using Sst.Contact.App.Dtos.ContactInformationDtos;
using Sst.Shared.Dtos;
using Sst.Shared.Dtos.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sst.Contact.App.Mapper
{
    public class GenerateMapper : Profile
    {

        public GenerateMapper()
        {
            CreateMap<Sst.Contact.Models.Contact, ContactDto>().ReverseMap();
            CreateMap<Sst.Contact.Models.Contact, ContactCreateDto>().ReverseMap();
            CreateMap<Sst.Contact.Models.Contact, ContactUpdateDto>().ReverseMap();

            CreateMap<Sst.Contact.Models.ContactInformation, ContactInformationDto>().ReverseMap();
            CreateMap<Sst.Contact.Models.ContactInformation, ContactInformationCreateDto>().ReverseMap();
            CreateMap<Sst.Contact.Models.ContactInformation, ContactInformationUpdateDto>().ReverseMap();

            CreateMap<Sst.Contact.Models.Contact, ContactCommand>().ReverseMap();

        }
    }
}
