using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Moq;
using Sst.Contact.App.Dtos.ContactDtos;
using Sst.Contact.App.Interfaces;
using Sst.Contact.App.Mapper;
using Sst.Contact.Controllers;
using Sst.Contact.Data.Settings;
using Sst.Contact.Models;
using Sst.Contact.Services;
using Sst.Shared.Dtos;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestProject2
{
    public class UnitTest1
    {
        public UnitTest1()
        {

        }

        [Fact]
        public async Task Test1()
        {
           Contact contact = new Contact();
            
            contact.Firm = "Test";
            contact.Name = "Test";
            contact.Surname = "Test1";
            //contact.ContactInformation.Id = ObjectId.GenerateNewId().ToString();
            //contact.ContactInformation.Content = "test";
            //contact.ContactInformation.Type = "Location";
            //contact.ContactInformation.InformationType.Id = ObjectId.GenerateNewId().ToString();
            //contact.ContactInformation.InformationType.Location = "Türkiye";
            //contact.ContactInformation.InformationType.PhoneNumber = "05544079613";



            var mockContactService = new Mock<IContactService>();

            mockContactService.Setup(service => service.CreateAsync(contact)).ReturnsAsync(new ResponseDto<ContactDto>());

            var sut = new ContactController(mockContactService.Object);

            var result = await sut.CreateAsync(contact);
     
            var t= Assert.IsType<ResponseDto<ContactDto>>(result);
        }


        public IConfigurationRoot Configuration { get; }
    }

}
