using MongoDB.Driver;
using Sst.Report.App.Interfaces;
using Sst.Report.Data.Settings;
using Sst.Report.Models;
using Sst.Shared.Dtos.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sst.Report.Services
{


    public class ContactCrudService : IContactCrudService
    {

        private readonly IMongoCollection<Contact> _contactCollection;

        public ContactCrudService(IMongoCollection<Contact> _contactCollection)
        {
        }

        public async void CreateAsync(ContactCommand contact)
        {
            await Task.Run(() =>
            {
                if (contact.Crud == "Insert")
                {
                    _contactCollection.InsertOneAsync(MapToContact(contact));
                }
            });
        }

        public void DeleteAsync(ContactCommand contact)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(ContactCommand contact)
        {
            throw new NotImplementedException();
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
