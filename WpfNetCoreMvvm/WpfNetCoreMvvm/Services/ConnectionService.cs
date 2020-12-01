using Airport_Common.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WpfNetCoreMvvm.Services
{
    public class ConnectionService : IConnectionService
    {
        private HubConnection connection;
        public event EventHandler<IEnumerable<AirportStatus>> ReceiveAirports;
        public ConnectionService()
        {
            InitialConnection();
            Connect();
        }

        private void InitialConnection()
        {
            connection = new HubConnectionBuilder()
               .WithUrl("https://localhost:44313/airport")
               .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
        }

        private async void Connect()
        {
            SetSingalRFuncs();
            try
            {
                await connection.StartAsync();
            }
            catch (Exception)
            {
                //todo: show error message
            }
        }

        private void SetSingalRFuncs()
        {
            connection.On<string>("ReceiveAirports", (jsonAirports) =>
            {
                IEnumerable<AirportStatus> airports = JsonConvert.DeserializeObject<IEnumerable<AirportStatus>>(jsonAirports,
                    new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });

                ReceiveAirports?.Invoke(this, airports);
            });
        }

    }
}
