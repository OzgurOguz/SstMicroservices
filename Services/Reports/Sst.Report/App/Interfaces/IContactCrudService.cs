using Sst.Report.Models;
using Sst.Shared.Dtos;
using Sst.Shared.Dtos.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sst.Report.App.Interfaces
{
    public interface IContactCrudService
    {
        void CreateAsync(ContactCommand contact);
        void UpdateAsync(ContactCommand contact);
        void DeleteAsync(ContactCommand contact);
    }
}
