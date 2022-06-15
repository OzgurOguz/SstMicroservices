using Sst.Contact.App.Dtos.ContactInformationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sst.Contact.App.Dtos.ContactDtos
{
    public class ContactDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Firm { get; set; }
        public virtual ContactInformationDto ContactInformationDto { get; set; }
    }
}
