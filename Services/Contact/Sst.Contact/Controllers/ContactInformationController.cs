using Microsoft.AspNetCore.Mvc;
using Sst.Contact.App.Dtos.ContactInformationDtos;
using Sst.Contact.App.Interfaces;
using Sst.Contact.Models;
using Sst.Shared.BaseControllers;
using Sst.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sst.Contact.Controllers
{
    [Route("api/[controller]")]
    public class ContactInformationController : Controller //CustomBaseController
    {
        private readonly IContactInformationService _contactInformationService;

        public ContactInformationController(IContactInformationService contactInformationService)
        {
            _contactInformationService = contactInformationService;
        }

        [HttpPost]
        public async Task<ResponseDto<ContactInformationDto>> CreateAsync(ContactInformation contactInformation)
        {
            return  await _contactInformationService.CreateAsync(contactInformation);
            //return (ResponseDto<ContactInformationDto>)CreateActionResultInstance(response);
        }

        [HttpDelete]
        public async Task<ResponseDto<NoContent>> DeleteAsync(string id)
        {
            return await _contactInformationService.DeleteAsync(id);
           // return (ResponseDto<NoContent>)CreateActionResultInstance(response);
        }

        [HttpGet]
        public async Task<ResponseDto<List<ContactInformationDto>>> GetAllAsync()
        {

            return await _contactInformationService.GetAllAsync();
           // return (ResponseDto<List<ContactInformationDto>>)CreateActionResultInstance(response);
        }

    }
}
