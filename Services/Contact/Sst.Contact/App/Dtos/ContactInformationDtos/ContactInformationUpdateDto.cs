using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sst.Contact.App.Dtos.ContactInformationDtos
{
    public class ContactInformationUpdateDto
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
    }
}
