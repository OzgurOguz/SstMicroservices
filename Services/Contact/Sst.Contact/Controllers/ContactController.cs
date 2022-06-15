using Microsoft.AspNetCore.Mvc;
using Sst.Contact.App.Dtos.ContactDtos;
using Sst.Contact.App.Interfaces;
using Sst.Shared.BaseControllers;
using Sst.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sst.Contact.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : Controller //CustomBaseController
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost]
        public async Task<ResponseDto<ContactDto>> CreateAsync(Models.Contact contact)
        {
            return await _contactService.CreateAsync(contact);
           // return (ResponseDto<ContactDto>)CreateActionResultInstance(response);
        }

        [HttpDelete]
        public async Task<ResponseDto<NoContent>> DeleteAsync(string id)
        {

            return await _contactService.DeleteAsync(id);
            //return response(ResponseDto<NoContent>)CreateActionResultInstance(response);
        }

        [HttpGet]
        public async Task<ResponseDto<List<ContactDto>>> GetAllAsync()
        {

            return await _contactService.GetAllAsync();
             //(ResponseDto<List<ContactDto>>)CreateActionResultInstance(response);
        }

        [HttpGet("{id}")]
        public async Task<ResponseDto<ContactDto>> GetByIdAsyncAsync(string id)
        {
            return  await _contactService.GetByIdAsyncAsync(id);
            //return (ResponseDto<ContactDto>)CreateActionResultInstance(response);

        }

        [HttpPut]
        public async Task<ResponseDto<NoContent>> UpdateAsync(ContactUpdateDto contactUpdateDto)
        {
            return await _contactService.UpdateAsync(contactUpdateDto);
           // return (ResponseDto<NoContent>)CreateActionResultInstance(response);
        }
    }
}
