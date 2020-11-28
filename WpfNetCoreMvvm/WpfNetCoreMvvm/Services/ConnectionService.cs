using Airport_Common.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpfNetCoreMvvm.Services
{
    public class ConnectionService : IConnectionService
    {
        private HubConnection connection;
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
            catch (Exception ex)
            {
                //todo: show error message
            }
        }

        private void SetSingalRFuncs()
        {
            connection.On<IEnumerable<AirportStatus>>("ReceiveAirports", (airports) =>
            {
                //todo: add airport status to list
            });
        }

    }
}
