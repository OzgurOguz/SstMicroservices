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
        public Task<List<LocationWithNumbersDto>> GetLocationWithNumbers()
        {
            return _reportService.GetLocationWithNumbers();
        }

        [HttpGet]
        public Task<List<PeopleAtLocationDto>> GetPeopleAtLocation()
        {
            return _reportService.GetPeopleAtLocation();
        }

        [HttpGet]
        public Task<List<PhoneNumberAtAdressBook>> GetPhoneNumberAtAdressBook()
        {
            return _reportService.GetPhoneNumberAtAdressBook();

        }
    }
}
