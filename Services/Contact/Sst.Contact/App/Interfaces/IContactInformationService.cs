using Sst.Contact.App.Dtos.ContactInformationDtos;
using Sst.Contact.Models;
using Sst.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sst.Contact.App.Interfaces
{
    public interface IContactInformationService
    {
        Task<ResponseDto<List<ContactInformationDto>>> GetAllAsync();
        Task<ResponseDto<ContactInformationDto>> CreateAsync(ContactInformation contactInformation);
        Task<ResponseDto<NoContent>> DeleteAsync(string id);
    }
}
