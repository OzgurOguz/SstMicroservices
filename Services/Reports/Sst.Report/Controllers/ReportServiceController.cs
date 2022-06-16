using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sst.Report.App;
using Sst.Report.App.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sst.Report.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportServiceController : Controller
    {

        public readonly IReportService _reportService;

        public ReportServiceController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        [Route("location-with-numbers")]
        public Task<List<LocationWithNumbersDto>> GetLocationWithNumbers()
        {
            return _reportService.GetLocationWithNumbers();
        }

        [HttpGet]
        [Route("people-at-location")]
        public Task<List<PeopleAtLocationDto>> GetPeopleAtLocation()
        {
            return _reportService.GetPeopleAtLocation();
        }

        [HttpGet]
        [Route("phone-number-at-adresbook")]
        public Task<List<PhoneNumberAtAdressBook>> GetPhoneNumberAtAdressBook()
        {
            return _reportService.GetPhoneNumberAtAdressBook();

        }
    }
}
