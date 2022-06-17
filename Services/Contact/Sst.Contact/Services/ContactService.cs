using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Sst.Contact.App.Dtos.ContactDtos;
using Sst.Contact.App.Interfaces;
using Sst.Contact.Data.Settings;
using Sst.Contact.Publisher;
using Sst.Shared.Dtos;
using Sst.Shared.Dtos.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sst.Contact.Services
{
    public class ContactService : IContactService
    {
        private readonly IMongoCollection<Sst.Contact.Models.Contact> _contactCollection;
        private readonly IMapper _mapper;
        private readonly IBus _busService;
        private readonly IConfiguration _configuration;


        public ContactService(IMapper mapper, IDatabaseSettings dbSettings, IBus busService, IConfiguration configuration)
        {
            var client = new MongoClient(dbSettings.ConnectionString);
            var db = client.GetDatabase(dbSettings.DatabaseName);
            _contactCollection = db.GetCollection<Sst.Contact.Models.Contact>(dbSettings.ContactCollectionName);
            _mapper = mapper;
            _busService = busService;
            _configuration = configuration;
        }

        public async Task<ResponseDto<List<ContactDto>>> GetAllAsync()
        {
            var contact = await _contactCollection.Find(x => true).ToListAsync();
            return ResponseDto<List<ContactDto>>.Success(_mapper.Map<List<ContactDto>>(contact), 200);
        }


        public async Task<ResponseDto<ContactDto>> GetByIdAsyncAsync(string id)
        {

            var contact = await _contactCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (contact == null) return ResponseDto<ContactDto>.Fail("Contact Info Not Found", 404);


            return ResponseDto<ContactDto>.Success(_mapper.Map<ContactDto>(contact), 200);
        }


        public async Task<ResponseDto<ContactDto>> CreateAsync(Sst.Contact.Models.Contact contact)
        {

            await _contactCollection.InsertOneAsync(contact);

            ContactCommand contactCommand = _mapper.Map<Sst.Contact.Models.Contact, ContactCommand>(contact);
            contactCommand.Crud = "Insert";
            Sst.Contact.Publisher.Publisher<ContactCommand> publisher = new Publisher<ContactCommand>(_busService, _configuration);
            publisher.Publish(contactCommand, "Insert");

            return ResponseDto<ContactDto>.Success(_mapper.Map<ContactDto>(contact), 200);
        }


        public async Task<ResponseDto<NoContent>> UpdateAsync(ContactUpdateDto contactUpdateDto)
        {
            var updateContact = _mapper.Map<Sst.Contact.Models.Contact>(contactUpdateDto);
            var result = await _contactCollection.FindOneAndReplaceAsync(x => x.Id == contactUpdateDto.Id, updateContact);


            if (result == null) return ResponseDto<NoContent>.Fail("Contact Not Found", 404);

            ContactCommand contactCommand = _mapper.Map<Sst.Contact.Models.Contact, ContactCommand>(updateContact);
            Sst.Contact.Publisher.Publisher<ContactCommand> publisher = new Publisher<ContactCommand>(_busService, _configuration);
            contactCommand.Crud = "Update";
            publisher.Publish(contactCommand, "Update");

            return ResponseDto<NoContent>.Success(204);
        }

        public async Task<ResponseDto<NoContent>> DeleteAsync(string id)
        {
            var result = await _contactCollection.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount == 0) return ResponseDto<NoContent>.Fail("Contact Not Found", 404);

            ContactCommand contactCommand = new ContactCommand()
            {
                Id = id
            };
            Sst.Contact.Publisher.Publisher<ContactCommand> publisher = new Publisher<ContactCommand>(_busService, _configuration);
            contactCommand.Crud = "Delete";
            publisher.Publish(contactCommand, "Delete");

            return ResponseDto<NoContent>.Success(204);
        }


    }
}
