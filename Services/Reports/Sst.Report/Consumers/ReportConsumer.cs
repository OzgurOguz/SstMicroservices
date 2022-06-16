using MassTransit;
using MongoDB.Driver;
using Sst.Report.Data.Settings;
using Sst.Report.Models;
using Sst.Shared.Dtos.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sst.Report.Consumers
{
    public class ReportConsumer : IConsumer<ContactCommand>
    {

        private readonly IMongoCollection<Contact> _contactCollection;


        public ReportConsumer(IDatabaseSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.ConnectionString);
            var db = client.GetDatabase(dbSettings.DatabaseName);
            _contactCollection = db.GetCollection<Contact>(dbSettings.ContactCollectionName);
        }

        public async Task Consume(ConsumeContext<ContactCommand> context)
        {

            await Task.Run(() =>
            {
                if (context.Message.Crud == "Insert")
                {
                    _contactCollection.InsertOneAsync(MapToContact(context.Message));
                }
            });

        }


        public Contact MapToContact(ContactCommand contactCommand)
        {

            Contact contact = new();

            contact.Id = contactCommand.Id;
            contact.Firm = contactCommand.Firm;
            contact.Name = contactCommand.Name;
            contact.Surname = contactCommand.Surname;
            //contact.ContactInformation.Id = contactCommand.ContactInformation.Id;
            //contact.ContactInformation.Content = contactCommand.ContactInformation.Content;
            //contact.ContactInformation.Type = contactCommand.ContactInformation.Type;
            //contact.ContactInformation.InformationType.Id = contactCommand.ContactInformation.InformationType.Id;
            //contact.ContactInformation.InformationType.Location = contactCommand.ContactInformation.InformationType.Location;
            //contact.ContactInformation.InformationType.PhoneNumber = contactCommand.ContactInformation.InformationType.PhoneNumber;

            return contact;
        }

    }
}
