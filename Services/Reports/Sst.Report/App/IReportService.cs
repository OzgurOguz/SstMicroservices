using Sst.Report.App.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sst.Report.App
{
    public interface IReportService
    {
        Task<List<LocationWithNumbersDto>> GetLocationWithNumbers();
        Task<List<PeopleAtLocationDto>> GetPeopleAtLocation();
        Task<List<PhoneNumberAtAdressBook>> GetPhoneNumberAtAdressBook()
    }
}
