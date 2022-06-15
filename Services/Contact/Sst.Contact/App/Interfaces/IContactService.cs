using Sst.Contact.App.Dtos.ContactDtos;
using Sst.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sst.Contact.App.Interfaces
{
    public interface IContactService
    {
        Task<ResponseDto<List<ContactDto>>> GetAllAsync();
        Task<ResponseDto<ContactDto>> GetByIdAsyncAsync(string id);
        Task<ResponseDto<ContactDto>> CreateAsync(Sst.Contact.Models.Contact contact);
        Task<ResponseDto<NoContent>> UpdateAsync(ContactUpdateDto contactUpdateDto);
        Task<ResponseDto<NoContent>> DeleteAsync(string id);
    }
}
