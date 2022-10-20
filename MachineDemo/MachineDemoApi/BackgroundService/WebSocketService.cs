using DemoEntity;
using DemoServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MachineDemoApi
{   
    public class WebsocketService : BackgroundService
    {
        private readonly ILogger<WebsocketService> logger;
        private readonly IConfiguration configuration;
        private readonly IServiceScopeFactory serviceScopeFactory;
      

        public WebsocketService(ILogger<WebsocketService> logger , IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var urlstr = configuration["ServerUrl"];
            while (!cancellationToken.IsCancellationRequested)
                using (var socket = new ClientWebSocket())
                    try
                    {
                        await socket.ConnectAsync(new Uri(urlstr), cancellationToken);
                        await Receive(socket, cancellationToken);

                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex.Message);
                    }
        }

        private async Task Receive(ClientWebSocket socket, CancellationToken cancellationToken)
        {
            var buffer = new ArraySegment<byte>(new byte[1024]);
            while (!cancellationToken.IsCancellationRequested)
            {
                WebSocketReceiveResult result;
                using (var ms = new MemoryStream())
                {
                    do
                    {
                        result = await socket.ReceiveAsync(buffer, cancellationToken);
                        ms.Write(buffer.Array, buffer.Offset, result.Count);
                    } 
                    while (!result.EndOfMessage);

                    if (result.MessageType == WebSocketMessageType.Close)
                        break;

                    ms.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(ms, Encoding.UTF8))
                    {
                        var payloadstr = await reader.ReadToEndAsync();
                        using (var scope = serviceScopeFactory.CreateScope())
                        {
                            var machinesService = scope.ServiceProvider.GetRequiredService<IMachinesService>();
                            await machinesService.SaveMachineAsync(payloadstr);
                        }
                                             
                       
                    }
                }
            };
        }
    }
}
