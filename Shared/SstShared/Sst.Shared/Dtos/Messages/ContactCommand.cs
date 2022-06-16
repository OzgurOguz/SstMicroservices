using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sst.Shared.Dtos.Messages
{
    public class ContactCommand
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Firm { get; set; }
        public string Crud { get; set; }
        public virtual ContactInformation ContactInformation { get; set; }
    }


}
