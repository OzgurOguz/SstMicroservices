using AutoMapper;
using MongoDB.Driver;
using Sst.Contact.App.Dtos.ContactInformationDtos;
using Sst.Contact.App.Interfaces;
using Sst.Contact.Data.Settings;
using Sst.Contact.Models;
using Sst.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sst.Contact.Services
{
    public class ContactInformationService : IContactInformationService
    {
        private readonly IMongoCollection<ContactInformation> _contactInformationCollection;
        private readonly IMapper _mapper;

        public ContactInformationService(IMapper mapper, IDatabaseSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.ConnectionString);
            var db = client.GetDatabase(dbSettings.DatabaseName);
            _contactInformationCollection = db.GetCollection<ContactInformation>(dbSettings.ContactInformationCollectionName);
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<ContactInformationDto>>> GetAllAsync()
        {
            var contactInformations = await _contactInformationCollection.Find(x => true).ToListAsync();
            return ResponseDto<List<ContactInformationDto>>.Success(_mapper.Map<List<ContactInformationDto>>(contactInformations), 200);
        }


        public async Task<ResponseDto<ContactInformationDto>> CreateAsync(ContactInformation contactInformation)
        {

            await _contactInformationCollection.InsertOneAsync(contactInformation);
            var x= ResponseDto<ContactInformationDto>.Success(_mapper.Map<ContactInformationDto>(contactInformation), 200);

            return x;
        }


        public async Task<ResponseDto<NoContent>> DeleteAsync(string id)
        {
            var result = await _contactInformationCollection.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount == 0) return ResponseDto<NoContent>.Fail("Contact Not Found", 404);

            return ResponseDto<NoContent>.Success(204);
        }


    }
}
