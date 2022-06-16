using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sst.Shared.Dtos.Messages
{
    public class ContactInformation
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public virtual InformationType InformationType { get; set; }
    }
}
