using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sst.Contact.Controllers;
using Sst.Contact.Data.Settings;
using Sst.Contact.Services;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        ContactService _contactRepo;
        ContactController _contactController;
        IOptions<DatabaseSettings> settings = Options.Create<DatabaseSettings>(new DatabaseSettings());
        ConfigurationRoot builder = (ConfigurationRoot)new ConfigurationBuilder().AddJsonFile("appSettings.json", true, true).AddEnvironmentVariables().Build();
        private readonly IMapper _mapper;
        private readonly IBus _busService;
        private readonly IConfiguration _configuration;
        private readonly IDatabaseSettings dbSettings;

        public IConfigurationRoot Configuration { get; }

        [TestMethod]
        public void TestMethod1()
        {

            settings.Value.ConnectionString = "mongodb://localhost:27017";
            settings.Value.DatabaseName = "BIKAFA";
            settings.Value.IConfigurationRoot = builder;
            _contactRepo = new ContactService(_mapper, dbSettings, _busService, _configuration);
            _contactController = new ContactController(_contactRepo);
        }

        [Fact]
        public async Task SaveDataTests()
        {
            UserModel userModelTest = new UserModel()
            {
                UserName = "ozgur41",
                UserSurname = "oguz6",
                UserCompany = "vzx6"
            };

            var query = _controller.SaveData(userModelTest);
            query.Wait();

            Assert.IsType<CreatedResult>(query.Result);
        }

    }
}
