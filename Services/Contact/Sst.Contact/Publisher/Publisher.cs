using MassTransit;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sst.Contact.Publisher
{
    public class Publisher<T> where T : class
    {

        private readonly IBus _busService;
        private readonly IConfiguration _configuration;

        public Publisher(IBus busService, IConfiguration configuration)
        {
            _busService = busService;
            _configuration = configuration;
        }

        public async void Publish(T data, string exchangeName)
        {
            Uri uri = new Uri(_configuration["ServiceBus:Uri"] + _configuration["ServiceBus:Queue"]);
            var endpoint = await _busService.GetSendEndpoint(uri);
            await endpoint.Send(data);
        }

    }
}
