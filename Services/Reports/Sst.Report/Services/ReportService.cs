

using MongoDB.Driver;
using Sst.Report.App;
using Sst.Report.App.Dtos;
using Sst.Report.Data.Settings;
using Sst.Report.Models;
using Sst.Shared.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sst.Report.Services
{
    public class ReportService : IReportService
    {

        private readonly IMongoCollection<ContactInformation> _contactInformationCollection;
        private readonly IMongoCollection<Contact> _contactCollection;

        public ReportService(IDatabaseSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.ConnectionString);
            var db = client.GetDatabase(dbSettings.DatabaseName);
            _contactInformationCollection = db.GetCollection<ContactInformation>(dbSettings.ContactInformationCollectionName);
            _contactCollection = db.GetCollection<Contact>(dbSettings.ContactCollectionName);
        }

        public async Task<List<LocationWithNumbersDto>> GetLocationWithNumbers()
        {
            var contactInformations = await _contactInformationCollection.Find(x => x.InformationType.Location != null).ToListAsync();

            var groups = contactInformations.GroupBy(x => x.InformationType.Location)
                          .Select(x => new
                          {
                              Location = x.Key,
                              LocationNumber = x.Count()
                          })
                          .OrderBy(n => n.LocationNumber);

            return new List<LocationWithNumbersDto>();

        }

        public async Task<List<PeopleAtLocationDto>> GetPeopleAtLocation()
        {
            var contacts = await _contactCollection.Find(x => x.ContactInformation.InformationType.Location != null).ToListAsync();

            var groups = contacts.GroupBy(x => x.ContactInformation.InformationType.Location, x => x.Name)
                          .Select(x => new
                          {
                              Name = x.Key,
                              NumberOfPeople = x.Count()
                          })
                          .OrderBy(n => n.NumberOfPeople).Distinct();

            return new List<PeopleAtLocationDto>();

        }
        public async Task<List<PhoneNumberAtAdressBook>> GetPhoneNumberAtAdressBook()
        {
            var contacts = await _contactCollection.Find(x => x.ContactInformation.InformationType.Location != null && x.ContactInformation.InformationType.PhoneNumber != null).ToListAsync();

            var groups = contacts.GroupBy(x => x.ContactInformation.InformationType.PhoneNumber, x => x.ContactInformation.InformationType.Location)
                          .Select(x => new
                          {
                              Name = x.Key,
                              NumberOfPeople = x.Count()
                          })
                          .OrderBy(n => n.NumberOfPeople).Distinct();

            return new List<PhoneNumberAtAdressBook>();

        }


    }
}
