using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseService.Core.Interfaces;
using WarehouseService.Core.Models;

namespace WarehouseService.Infrastructure.Serices
{
    public class WarehouseApiClient : IWarehouseApiClient
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public WarehouseApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClient = httpClientFactory.CreateClient();
            this.configuration = configuration;
        }
        public async Task StartShipment(Core.Models.Shipment shipment)
        {
            var content = new StringContent("", Encoding.UTF8, "application/json");
            var url = configuration["WarehouseApiUrl"] + $"/ShipmentInfo/{shipment.Id}";
            await this.httpClient.PostAsync(url, content);
        }
    }
}
